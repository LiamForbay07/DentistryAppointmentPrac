using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Visits.DTO;

namespace DentistryAppointmentPrac.Shared.Visits.Interfaces
{
    public interface IVisitService
    {
        //Returns queue number
        Task<string> CreateVisit(CreateVisitRequest request);
        Task<string> CreateWalkInVisit(CreateWalkInVisitRequest request);
        Task<bool> UpdateVisitArrivalTime(UpdateVisitRequest request);
        Task<bool> DeleteVisit(Guid id);
        Task<bool> CancelVisit(Guid id);
        Task<PageData<VisitSummary?>> SearchVisitByFacilityId(Guid facilityId, VisitSummaryPagedRequest request);
        Task<PageData<VisitSummary?>> SearchVisitsForAllFacilities(VisitSummaryPagedRequest request);
    }
}
