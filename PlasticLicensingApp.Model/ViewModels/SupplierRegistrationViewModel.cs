using System.ComponentModel.DataAnnotations;

namespace PlasticLicensingApp.ViewModels
{
    public class SupplierRegistrationViewModel
    {
        [Required(ErrorMessage = "Company Name is required")]
        public string SupplierCompanyName { get; set; }

        public bool UseProfileAddress { get; set; }

        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }
        public string? Country { get; set; }
        
        [Required(ErrorMessage = "Phone number is required")]
        public string? Phone { get; set; }
    }
}
