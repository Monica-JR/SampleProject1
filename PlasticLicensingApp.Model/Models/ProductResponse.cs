using PlasticLicensingApp.Models.Base;
using System;

namespace PlasticLicensingApp.Models
{
    public class ProductResponse : AuditableEntity
    {
        public int ProductID { get; set; }
        public string Comment { get; set; }
        public int FileID { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;
        public virtual Product Product { get; set; }
        public virtual File File { get; set; }
    }
}
