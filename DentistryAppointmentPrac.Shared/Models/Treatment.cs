using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Treatment : BaseModel
    {
        public Guid AppointmentId { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string TreatmentPerformed { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CompletedDate { get; set; }
    }
}
