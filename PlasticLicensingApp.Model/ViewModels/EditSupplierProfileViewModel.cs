using System.ComponentModel.DataAnnotations;

namespace PlasticLicensingApp.ViewModels
{
    public class EditSupplierProfileViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        public string SupplierCompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address Line 1 is required")]
        [Display(Name = "Address Line 1")]
        public string Line1 { get; set; } = string.Empty;

        [Display(Name = "Address Line 2 (Optional)")]
        public string? Line2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pincode is required")]
        [Display(Name = "Pincode")]
        public string Pincode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;
    }
}
