using System;

namespace PlasticLicensingApp.Models
{
    public class Log
    {
        public int ID { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string EntityType { get; set; }
        public int? EntityID { get; set; }
        public int? UserID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
