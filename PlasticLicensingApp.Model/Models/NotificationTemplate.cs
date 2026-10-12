using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class NotificationTemplate : AuditableEntity
    {
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
