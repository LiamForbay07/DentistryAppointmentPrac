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
    }
}
