using System.Threading.Tasks;

namespace PlasticLicensingApp.Services
{
    public interface IDatabaseInitializationService
    {
        Task InitializeDatabaseAsync();
    }
}
