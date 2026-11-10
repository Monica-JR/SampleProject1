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
        public string? LicenseStatus { get; set; }
        public string? RawLicenseNumber { get; set; }
        public bool IsLicenseActive => string.Equals(LicenseStatus, "Active", StringComparison.OrdinalIgnoreCase);
        public string DisplayLicenseNumber => IsLicenseActive && !string.IsNullOrWhiteSpace(RawLicenseNumber) ? RawLicenseNumber : string.Empty;
    }
}
