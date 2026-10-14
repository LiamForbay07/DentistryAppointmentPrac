using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Facilities.DTO
{
    public class CreateFacilityRequest : IRequestHandler<Facility, CreateFacilityRequest>
    {
        public string Name { get; set; } = string.Empty;
        public string Provice { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string? GPSLocation { get; set; }
        public bool IsActive { get; set; }

        public static Facility ToEntity(CreateFacilityRequest request)
        {
            if (request == null) return new();
            return new()
            {
                Id = Guid.NewGuid(),
                GPSLocation = request.GPSLocation,
                District = request.District,
                Name = request.Name,
                Provice = request.Provice,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                LastUpdated = DateTime.UtcNow
            };
        }
    }
}
