using System;
using System.Collections.Generic;

namespace PlasticLicensingApp.ViewModels
{
    public class SupplierProductsViewModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public List<SupplierProductItemViewModel> Products { get; set; } = new List<SupplierProductItemViewModel>();
        public ProductSubmissionViewModel CreateProductModel { get; set; } = new ProductSubmissionViewModel();
    }

    public class SupplierProductItemViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public List<ProductParameterDisplayViewModel> Parameters { get; set; } = new List<ProductParameterDisplayViewModel>();
    }

    public class ProductParameterDisplayViewModel
    {
        public string ParameterName { get; set; } = string.Empty;
        public string ParameterValue { get; set; } = string.Empty;
    }
}
