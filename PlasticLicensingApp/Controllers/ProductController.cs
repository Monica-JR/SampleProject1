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

            var viewModel = new ProductSubmissionViewModel
            { 
                SupplierID = supplierId
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (product == null) return NotFound();

            var parameters = await _context.Parameters.Where(p => p.IsActive).ToListAsync();
            var existingValues = await _context.ProductParameters.Where(pp => pp.ProductID == id).ToListAsync();

            var viewModel = new ProductSubmissionViewModel
            { 
                ProductID = product.ID,
                SupplierID = product.SupplierID,
                ProductName = product.ProductName,
                StatusName = product.Status?.StatusName,
                Parameters = parameters.Select(p => new ParameterValueViewModel
                {
                    ParameterID = p.ID,
                    ParameterName = p.ParameterName,
                    Value = existingValues.FirstOrDefault(ev => ev.ParameterID == p.ID)?.ParameterValue
                }).ToList()
            };

            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductSubmissionViewModel model)
        {
            if (string.IsNullOrEmpty(model.ProductName)) return BadRequest(new { success = false, message = "Product name is required." });

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var statusNew = await _context.Statuses
                .FirstOrDefaultAsync(s => s.StatusName == "New" && s.StatusType == "Product");

            if (statusNew == null) return BadRequest(new { success = false, message = "Required status 'New' not found in system." });

            var product = new Product
            {
                ProductName = model.ProductName,
                SupplierID = model.SupplierID,
                StatusID = statusNew.ID,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, productId = product.ID });
        }

        [HttpPost]
        public async Task<IActionResult> SaveParameters([FromBody] ProductSubmissionViewModel model)
        {
            var product = await _context.Products
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.ID == model.ProductID);

            if (product == null) return NotFound();

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            if (model.Parameters != null)
            {
                foreach (var param in model.Parameters)
                {
                    var existing = await _context.ProductParameters
                        .FirstOrDefaultAsync(pp => pp.ProductID == product.ID && pp.ParameterID == param.ParameterID);

                    if (existing != null)
                    {
                        existing.ParameterValue = param.Value;
                        existing.LastChangedBy = userId;
                        existing.LastChangedDate = DateTime.Now;
                    }
                    else
                    {
                        _context.ProductParameters.Add(new ProductParameter
                        {
                            ProductID = product.ID,
                            ParameterID = param.ParameterID,
                            ParameterValue = param.Value,
                            CreatedBy = userId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }

            var statusSubmitted = await _context.Statuses
                .FirstOrDefaultAsync(s => s.StatusName == "Submitted" && s.StatusType == "Product");

            // Only update status if it is currently 'New' or has no status (id 0)
            if (statusSubmitted != null && (product.Status == null || product.Status.StatusName == "New" || product.StatusID == 0))
            {
                product.StatusID = statusSubmitted.ID;
                product.LastChangedBy = userId;
                product.LastChangedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Product parameters saved successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetParameters() 
        {
            var parameters = await _context.Parameters
                .Where(p => p.IsActive)
                .Select(p => new { ID = p.ID, ParameterName = p.ParameterName })
                .ToListAsync();
            return Ok(parameters);
        }
    }
}
