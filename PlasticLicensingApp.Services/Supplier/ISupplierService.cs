using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Services.Supplier
{
    public interface ISupplierService
    {
        Task<List<SupplierDashboardViewModel>> GetSuppliersForUserAsync(int userId);
        Task<(bool Success, string? ErrorMessage)> RegisterSupplierCompanyAsync(SupplierRegistrationViewModel model, int userId);
        Task<Address?> GetUserProfileAddressAsync(int userId);
        Task<EditSupplierProfileViewModel?> GetSupplierForEditAsync(int supplierId, int userId);
        Task<(bool Success, string? ErrorMessage)> UpdateSupplierCompanyAsync(EditSupplierProfileViewModel model, int userId);
    }
}
