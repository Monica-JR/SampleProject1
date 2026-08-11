using PlasticLicensingApp.Models.Base;
using System;

namespace PlasticLicensingApp.Models
{
    public class Notification : AuditableEntity
    {
        public int TemplateID { get; set; }
        public int SupplierID { get; set; }
        public int? ProductID { get; set; }
        public int? LicenseID { get; set; }
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? SentDate { get; set; }
        public string Status { get; set; }
    }
}