using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlasticLicensingApp.Services.Supplier;
using PlasticLicensingApp.ViewModels;
using System.Security.Claims;

namespace PlasticLicensingApp.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var suppliers = await _supplierService.GetSuppliersForUserAsync(userId);

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

            var (success, errorMessage) = await _supplierService.RegisterSupplierCompanyAsync(model, userId);
            if (!success)
            {
                ModelState.AddModelError("", errorMessage ?? "An error occurred while saving. Please try again.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetProfileAddress()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var address = await _supplierService.GetUserProfileAddressAsync(userId);
            if (address == null) return NotFound();

            return Json(new
            {
                line1 = address.Line1,
                line2 = address.Line2,
                city = address.City,
                state = address.State,
                pincode = address.Pincode,
                country = address.Country,
                phone = address.Phone
            });
        }
    }
}
