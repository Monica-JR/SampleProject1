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
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List(int supplierId)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.ID == supplierId);
            if (supplier == null) return NotFound();

            var products = await _context.Products
                .Include(p => p.Status)
                .Where(p => p.SupplierID == supplierId)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            ViewBag.SupplierId = supplierId;
            ViewBag.SupplierName = supplier.SupplierCompanyName;

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int supplierId)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.ID == supplierId);
            if (supplier == null) return NotFound();

            var parameters = await _context.Parameters.Where(p => p.IsActive).ToListAsync();
            
            var viewModel = new ProductSubmissionViewModel
            {
                SupplierID = supplierId,
                Parameters = parameters.Select(p => new ParameterValueViewModel
                {
                    ParameterID = p.ID,
                    ParameterName = p.ParameterName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductSubmissionViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();
            
            // Get "Submitted" status for Product
            var status = await _context.Statuses
                .FirstOrDefaultAsync(s => s.StatusName == "Submitted" && s.StatusType == "Product");

            var product = new Product
            {
                ProductName = model.ProductName,
                SupplierID = model.SupplierID,
                StatusID = status?.ID ?? 0,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            foreach (var param in model.Parameters)
            {
                var productParam = new ProductParameter
                {
                    ProductID = product.ID,
                    ParameterID = param.ParameterID,
                    ParameterValue = param.Value,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };
                _context.ProductParameters.Add(productParam);
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Product submitted successfully!" });
        }
    }
}