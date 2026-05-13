using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.DTO
{
    public class ImportProcessSummaryPagedRequest : PageRequest
    {
        public ImportStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
