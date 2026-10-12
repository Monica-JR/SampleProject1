using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Services.Account
{
    public interface IAccountService
    {
        Task<User?> AuthenticateAsync(LoginViewModel model);
        Task<bool> EmailExistsAsync(string email);
        Task<User> RegisterAsync(RegisterViewModel model);
    }
}
