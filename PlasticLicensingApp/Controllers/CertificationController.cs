using Microsoft.AspNetCore.Mvc;
using PlasticLicensingApp.Models;
using PlasticLicensingApp.Services.Product;

namespace PlasticLicensingApp.Controllers
{
    public class CertificationController : Controller
    {
        private readonly IProductService _productService;

        public CertificationController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var createdProduct = await _productService.CreateProductAsync(product);
                return Ok(createdProduct);
            }
            return BadRequest(ModelState);
        }
    }
}
