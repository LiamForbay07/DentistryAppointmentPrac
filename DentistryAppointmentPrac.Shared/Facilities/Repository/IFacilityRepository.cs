using DentistryAppointmentPrac.Shared.Facilities.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;

namespace DentistryAppointmentPrac.Shared.Facilities.Repository
{
    public interface IFacilityRepository : IBaseWriteableRepository<Facility, FacilitySummary>
    {
    }
}
