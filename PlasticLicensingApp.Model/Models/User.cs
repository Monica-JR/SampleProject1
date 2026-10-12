using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class User : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleID { get; set; }
        public int? AddressID { get; set; }
        public virtual Role Role { get; set; }
        public virtual Address? Address { get; set; }
    }
}
