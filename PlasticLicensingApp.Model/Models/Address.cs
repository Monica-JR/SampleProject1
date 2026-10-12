using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class Address : AuditableEntity
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
