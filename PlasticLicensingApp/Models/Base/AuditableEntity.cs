using System;

namespace PlasticLicensingApp.Models.Base
{
    public abstract class AuditableEntity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime? LastChangedDate { get; set; }
        public int? LastChangedBy { get; set; }
    }
}
