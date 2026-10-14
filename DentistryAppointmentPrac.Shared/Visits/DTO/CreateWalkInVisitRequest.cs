using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Patients.DTO;

namespace DentistryAppointmentPrac.Shared.Visits.DTO
{
    public record CreateWalkInVisitRequest : IRequestHandler<Visit, CreateWalkInVisitRequest>
    {
        public Guid? AppointmentId { get; set; }
        public VisitType VisitType { get; set; }
        public DateTime ArrivalTime { get; set; }
        public AppointmentStages Status { get; set; }
        public Guid FacilitiyId { get; set; }
        public PatientRequest? PatientDetails { get; set; }


        public static Visit ToEntity(CreateWalkInVisitRequest request)
        {
            var model = new Visit();
            if (request == null) return model;

            model = new()
            {
                Id = Guid.NewGuid(),
                AppointmentId = request.AppointmentId ?? Guid.Empty,
                Status = request.Status,
                ArrivalTime = request.ArrivalTime,
                CreatedDate = DateTime.UtcNow,
                FacilityId = request.FacilitiyId,
                VisitType = request.VisitType,
                LastUpdated = DateTime.UtcNow,
                QueueNumber = $"WN-{Guid.NewGuid().ToString("N")[..4]}",
            };
            return model;
        }
    }
}
