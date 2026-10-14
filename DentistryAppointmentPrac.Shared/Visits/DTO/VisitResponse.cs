using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System.Linq.Expressions;

namespace DentistryAppointmentPrac.Shared.Visits.DTO
{
    public class VisitResponse(Visit model) : ISummaryHandler<Visit, VisitResponse>, IResponseHandler<Visit, VisitResponse>
    {
        public Guid PatientId { get; set; }
        public Guid FacilityId { get; set; }
        public Guid? AppointmentId { get; set; }
        public VisitType VisitType { get; set; }
        public DateTime ArrivalTime { get; set; }
        public AppointmentStages Status { get; set; }
        public string QueueNumber { get; set; } = string.Empty; //QUEUE NUMBER FOR WALK-IN OR BOOKEDVISITS

        public static Expression<Func<Visit, VisitResponse>> SummaryMap => model => new VisitResponse(model);

        public static VisitResponse? FromEntity(Visit model)
        {
            if (model == null) return null;
            return new VisitResponse(model);
        }
    }
}
