using System.ComponentModel.DataAnnotations;

namespace PlasticLicensingApp.ViewModels
{
    public class SupplierEditViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string SupplierCompanyName { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string Line1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Line2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Pincode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string LicenseNumber { get; set; }
        public string LicenseStatus { get; set; }
    }
}