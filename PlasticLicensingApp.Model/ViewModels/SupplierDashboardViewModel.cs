using System;

namespace PlasticLicensingApp.ViewModels
{
    public class SupplierDashboardViewModel
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
