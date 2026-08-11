using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;

namespace PlasticLicensingApp.Controllers
{
    public class CertificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Status)
                .ToListAsync();
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                product.StatusID = 1; // Default to Pending
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }
    }
}
