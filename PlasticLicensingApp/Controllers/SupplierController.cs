using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System.Security.Claims;

namespace PlasticLicensingApp.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var suppliers = await _context.Suppliers
                .Include(s => s.Address)
                .Where(s => s.CreatedBy == userId)
                .Select(s => new SupplierDashboardViewModel
                {
                    ID = s.ID,
                    CompanyName = s.SupplierCompanyName,
                    City = s.Address.City,
                    State = s.Address.State,
                    CreatedDate = s.CreatedDate
                }).ToListAsync();

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] SupplierRegistrationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int addressId;
                if (model.UseProfileAddress)
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user == null) return BadRequest("User not found");
                    
                    if (user.AddressID == null) 
                    {
                        ModelState.AddModelError("", "No profile address found. Please enter a new address.");
                        return View(model);
                    }
                    addressId = user.AddressID.Value;
                }
                else
                {
                    var address = new Address
                    {
                        Line1 = model.Line1 ?? "",
                        Line2 = model.Line2,
                        City = model.City ?? "",
                        State = model.State ?? "",
                        Pincode = model.Pincode ?? "",
                        Country = model.Country ?? "",
                        Phone = model.Phone ?? "",
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now
                    };
                    _context.Addresses.Add(address);
                    await _context.SaveChangesAsync();
                    addressId = address.ID;
                }

                var supplier = new Supplier
                {
                    SupplierCompanyName = model.SupplierCompanyName,
                    AddressID = addressId,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };

                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "An error occurred while saving. Please try again.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProfileAddress()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.ID == userId);
            if (user?.Address == null) return NotFound();

            return Json(new
            {
                line1 = user.Address.Line1,
                line2 = user.Address.Line2,
                city = user.Address.City,
                state = user.Address.State,
                pincode = user.Address.Pincode,
                country = user.Address.Country,
                phone = user.Address.Phone
            });
        }
    }
}