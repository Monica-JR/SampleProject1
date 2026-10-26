using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlasticLicensingApp.Services.Product;
using PlasticLicensingApp.ViewModels;
using System.Security.Claims;

namespace PlasticLicensingApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int supplierId)
        {
            var viewModel = await _productService.GetProductSubmissionViewModelAsync(supplierId);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductSubmissionViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            await _productService.SubmitProductAsync(model, userId);

            return Ok(new { success = true, message = "Product submitted successfully!" });
        }
    }
}
