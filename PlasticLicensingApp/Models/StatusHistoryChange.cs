using PlasticLicensingApp.Models.Base;
using System;

namespace PlasticLicensingApp.Models
{
    public class StatusHistoryChange : AuditableEntity
    {
        public int EntityID { get; set; }
        public string EntityType { get; set; }
        public int OldStatusID { get; set; }
        public string OldStatusName { get; set; }
        public int NewStatusID { get; set; }
        public string NewStatusName { get; set; }
        public int ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; } = DateTime.UtcNow;
    }
}