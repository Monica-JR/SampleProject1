using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlasticLicensingApp.ViewModels
{
    public class ProductSubmissionViewModel
    {
        public int? ProductID { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public string StatusName { get; set; }

        public List<ParameterValueViewModel> Parameters { get; set; } = new List<ParameterValueViewModel>();
    }

    public class ParameterValueViewModel
    {
        public int ParameterID { get; set; }
        public string ParameterName { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
