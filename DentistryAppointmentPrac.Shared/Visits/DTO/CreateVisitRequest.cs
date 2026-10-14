using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Visits.DTO
{
    public record CreateVisitRequest
    (
       Guid PatientId,
       Guid? AppointmentId,
       VisitType VisitType,
       DateTime ArrivalTime,
       AppointmentStages Status,
       Guid FacilitiyId
    );
}
