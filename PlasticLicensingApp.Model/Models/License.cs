using PlasticLicensingApp.Models.Base;
using System;

namespace PlasticLicensingApp.Models
{
    public class License : AuditableEntity
    {
        public string LicenseNumber { get; set; } // Computed in DB
        public int SupplierID { get; set; }
        public int StatusID { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Status Status { get; set; }
    }
}
