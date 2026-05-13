using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.DTO
{
    public class ImportActivitySummary
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Job { get; set; } = string.Empty;
        public ImportType Type { get; set; }
        public ImportStatus Status { get; set; }
        public Records? Records { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
    }

    public class Records
    {
        public int? TotalRecordCount { get; set; }
        public int? SuccessRecordCount { get; set; }
        public int? FailedRecordCount { get; set; }
    }
}
