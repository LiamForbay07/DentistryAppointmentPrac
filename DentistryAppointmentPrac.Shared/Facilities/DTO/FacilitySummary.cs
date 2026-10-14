using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System.Linq.Expressions;

namespace DentistryAppointmentPrac.Shared.Facilities.DTO
{
    public class FacilitySummary : IResponseHandler<Facility, FacilitySummary>, ISummaryHandler<Facility, FacilitySummary>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Provice { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string? GPSLocation { get; set; }
        public bool IsActive { get; set; }

        public static Expression<Func<Facility, FacilitySummary>> SummaryMap => model => new FacilitySummary
        {
            Id = model.Id,
            District = model.District,
            GPSLocation = model.GPSLocation,
            IsActive = model.IsActive,
            Name = model.Name,
            Provice = model.Provice
        };

        public static FacilitySummary? FromEntity(Facility model)
        {
            if (model == null) return null;
            return SummaryMap.Compile().Invoke(model);
        }
    }
}
