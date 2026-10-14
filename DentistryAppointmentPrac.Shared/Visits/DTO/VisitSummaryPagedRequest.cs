using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Visits.DTO
{
    public class VisitSummaryPagedRequest : PageRequest
    {
        public Guid? FacilityId { get; set; }
        public AppointmentTypes? AppointmentType { get; set; }
        public AppointmentStages? AppointmentStage { get; set; }
        public VisitType? VisitType { get; set; }
        public DateTime? ArrivalTime { get; set; }
    }
}
