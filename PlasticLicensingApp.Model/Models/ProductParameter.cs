using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class ProductParameter : AuditableEntity
    {
        public int ProductID { get; set; }
        public int ParameterID { get; set; }
        public string ParameterValue { get; set; }
        public virtual Product Product { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}
