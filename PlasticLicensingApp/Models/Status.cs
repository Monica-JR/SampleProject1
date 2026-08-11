using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class Status : AuditableEntity
    {
        public string StatusName { get; set; }
        public string StatusType { get; set; }
        public bool IsActive { get; set; } = true;
    }
}