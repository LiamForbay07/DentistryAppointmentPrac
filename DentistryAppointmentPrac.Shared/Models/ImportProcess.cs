using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class ImportProcess : BaseModel
    {
        public Guid DocumentId { get; set; }
        public Guid? DepartmentId { get; set; } //Subject to change
        public ImportStatus Status { get; set; }
        public ImportType Type { get; set; }
        public List<string>? Errors { get; set; }
        public List<ImportChanges> ImportChanges { get; set; } = [];
        public Dictionary<string, string> Mapping { get; set; } = [];
        public char? FileDelimiter { get; set; }
        public int? NumberOfUpdates { get; set; }
        public int? NumberOfInserts { get; set; }
        public int? NumberOfErrors { get; set; }
        public int? NumberOfRowsRemoved { get; set; }
        public DocumentModel? Document { get; set; }
        public bool PartialImport { get; set; } = false;
        public bool QuickImport { get; set; } = false; //Skips queue for background service (Should limit rows)
    }

    public class ImportChanges
    {
        public Guid EntryId { get; set; } = Guid.NewGuid();
        public string FieldName { get; set; } = string.Empty;
        public string OriginalValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
    }

    public enum ImportType
    {
        General,
        Appointment,
        User,
        Patient,
        Booking,
        Dentist,
        Department
    }

    public enum ImportStatus
    {
        QuickImport,
        Pending,
        Rejected,
        Failed, 
        Validated,
        Approved,
        Completed
    }
}
