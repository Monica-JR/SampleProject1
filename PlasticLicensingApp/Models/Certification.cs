using System.ComponentModel.DataAnnotations;

namespace PlasticLicensingApp.Models;

public enum CertificationStatus
{
    Pending,
    Approved,
    Rejected
}

public class Certification
{
    public int Id { get; set; }

    public int PlasticItemId { get; set; }
    public PlasticItem? PlasticItem { get; set; }

    public CertificationStatus Status { get; set; } = CertificationStatus.Pending;

    public DateTime RequestDate { get; set; } = DateTime.UtcNow;

    public DateTime? DecisionDate { get; set; }

    public string? Comments { get; set; }
}
