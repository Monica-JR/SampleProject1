using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class Supplier : AuditableEntity
    {
        public string SupplierCompanyName { get; set; }
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}