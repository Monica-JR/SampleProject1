using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class Role : AuditableEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
