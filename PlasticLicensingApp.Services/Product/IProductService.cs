using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Services.Product
{
    public interface IProductService
    {
        Task<ProductSubmissionViewModel?> GetProductSubmissionViewModelAsync(int supplierId);
        Task<bool> SubmitProductAsync(ProductSubmissionViewModel model, int userId);
        Task<List<PlasticLicensingApp.Models.Product>> GetAllProductsAsync();
        Task<PlasticLicensingApp.Models.Product> CreateProductAsync(PlasticLicensingApp.Models.Product product);
    }
}
