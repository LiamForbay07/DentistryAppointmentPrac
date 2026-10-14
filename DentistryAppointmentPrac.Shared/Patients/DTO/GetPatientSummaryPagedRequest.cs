using DentistryAppointmentPrac.Shared.DTO;

namespace DentistryAppointmentPrac.Shared.Patients.DTO
{
    public class GetPatientSummaryPagedRequest : PageRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
