using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Visits.DTO
{
    public class GetExistingVisitsResponse
    {
        public Guid VisitId { get; set; }
        public Guid PatientId { get; set; }
        public Guid? AppointmentId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientSurname { get; set; } = string.Empty;
        public string PatientIdNumber { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;
        public string CellphoneNumber { get; set; } = string.Empty;
        public DateTime ArrivalTime { get; set; }
        public AppointmentStages Status { get; set; }
        public AppointmentTypes AppointmentType { get; set; }
        public string QueueNumber { get; set; } = string.Empty; //if booking  then BN-number, if walk-in then WN-number
    }
}
