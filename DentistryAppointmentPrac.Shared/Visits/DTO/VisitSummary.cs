using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Visits.DTO
{
    public class VisitSummary
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientSurname { get; set; } = string.Empty;
        public string PatientIdNumber { get; set; } = string.Empty;
        public string PatientCellNumber { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;
        public Guid FacilityId { get; set; }
        public Guid? AppointmentId { get; set; }
        public VisitType VisitType { get; set; }
        public DateTime ArrivalTime { get; set; }
        public AppointmentStages Status { get; set; }
        public string QueueNumber { get; set; } = string.Empty;
    }
}
