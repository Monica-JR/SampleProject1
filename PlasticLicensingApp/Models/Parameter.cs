using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class Parameter : AuditableEntity
    {
        public string ParameterName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}