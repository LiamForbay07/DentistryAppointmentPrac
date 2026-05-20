using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class DocumentModel : BaseModel
    {
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentEntry { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public Guid? DentalDepartmentId { get; set; }
        public bool HasOfflineBackup { get; set; }
    }

    public class OfflineFile : BaseModel
    {
        public Guid? DocumentId { get; set; }
        public DocumentModel? Document { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public byte[] File { get; set; } = [];
    }
}
