using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class Product : AuditableEntity
    {
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int StatusID { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Status Status { get; set; }
    }
}