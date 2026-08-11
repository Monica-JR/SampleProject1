using System.ComponentModel.DataAnnotations;

namespace PlasticLicensingApp.Models;

public class PlasticItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    public required string MaterialType { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public Certification? Certification { get; set; }
}
