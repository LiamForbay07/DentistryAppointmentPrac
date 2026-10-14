using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using DentistryAppointmentPrac.Shared.Visits.DTO;

namespace DentistryAppointmentPrac.Shared.Visits.Interfaces
{
    public interface IVisitRepository : IBaseWriteableRepository<Visit, VisitResponse>
    {
        Task<PageData<VisitSummary?>> GetPagedVisitByFacilityId(Guid facilityId, VisitSummaryPagedRequest request);
        Task<PageData<VisitSummary?>> GetPagedVisitsForAllFacilities(VisitSummaryPagedRequest request);
    }
}
