using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class OfflineFile : BaseModel
    {
        public Guid? DocumentId { get; set; }
        public DocumentModel? Document { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public byte[] Data { get; set; } = [];
    }
}
