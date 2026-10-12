using PlasticLicensingApp.Models.Base;

namespace PlasticLicensingApp.Models
{
    public class File : AuditableEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int UploadedBy { get; set; }
        public string ModuleType { get; set; }
        public int ReferenceID { get; set; }
    }
}
