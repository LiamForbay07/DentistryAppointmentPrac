using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository;
using DentistryAppointmentPrac.Shared.Visits.DTO;
using DentistryAppointmentPrac.Shared.Visits.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentistryAppointmentPrac.Shared.Visits.BusinessLogic.Repository
{
    public class VisitRepository<TContext>(TContext dbContext, UserContext userContext) : BaseWritableRepository<Visit, VisitResponse>(dbContext, userContext), IVisitRepository
        where TContext : DbContext
    {
        public async Task<PageData<VisitSummary?>> GetPagedVisitByFacilityId(Guid facilityId, VisitSummaryPagedRequest request)
        {
            IQueryable<Visit> visitQuery;
            if (facilityId == Guid.Empty)
                return new();
            visitQuery = GetVisitsByFacilityId(facilityId);

            if(request.AppointmentStage.HasValue)
            {
                visitQuery = visitQuery.Where(v => v.Status == request.AppointmentStage.Value);
            }

            if (request.VisitType.HasValue)
            {
                visitQuery = visitQuery.Where(v => v.VisitType == request.VisitType.Value);
            }

            if (request.ArrivalTime.HasValue)
            {
                visitQuery = visitQuery.Where(v => v.ArrivalTime.Date == request.ArrivalTime.Value.Date);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                visitQuery = visitQuery.Include(v => v.Patient).Where(v => v.Patient.Name.ToLower().Contains(search) ||
                                                    v.Patient.Surname.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                                    !string.IsNullOrEmpty(v.Patient.Cellphone) && v.Patient.Cellphone.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                                    !string.IsNullOrEmpty(v.Patient.Email) && v.Patient.Email.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            }
            var totalCount = await visitQuery.CountAsync();
            var items = await visitQuery.Skip((request.PageNo - 1) * request.PageSize)
                                        .Take(request.PageSize)
                                        .Select(v => new VisitSummary
                                        {
                                            PatientId = v.PatientId,
                                            PatientName = v.Patient.Name,
                                            PatientSurname = v.Patient.Surname,
                                            PatientIdNumber = v.Patient.IdNumber,
                                            PatientCellNumber = v.Patient.Cellphone ?? "",
                                            PatientEmail = v.Patient.Email ?? "",
                                            FacilityId = v.FacilityId,
                                            AppointmentId = v.AppointmentId,
                                            VisitType = v.VisitType,
                                            ArrivalTime = v.ArrivalTime,
                                            Status = v.Status,
                                            QueueNumber = v.QueueNumber
                                        })
                                        .ToListAsync();

            return new PageData<VisitSummary?>
            {
                Data = items,
                PageNumber = request.PageNo,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };  
        }

        public async Task<PageData<VisitSummary?>> GetPagedVisitsForAllFacilities(VisitSummaryPagedRequest request)
        {
            IQueryable<Visit> visitQuery = _dbSet.Where(v => !v.IsDeleted)
                                                .OrderByDescending(x => x.ArrivalTime);

            if (request.AppointmentStage.HasValue)
            {
                visitQuery = visitQuery.Where(v => v.Status == request.AppointmentStage.Value);
            }

            if (request.VisitType.HasValue)
            {
                visitQuery = visitQuery.Where(v => v.VisitType == request.VisitType.Value);
            }

            if (request.ArrivalTime.HasValue)
            {
                visitQuery = visitQuery.Where(v => v.ArrivalTime.Date == request.ArrivalTime.Value.Date);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                visitQuery = visitQuery.Include(v => v.Patient).Where(v => v.Patient.Name.ToLower().Contains(search) ||
                                                    v.Patient.Surname.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                                    !string.IsNullOrEmpty(v.Patient.Cellphone) && v.Patient.Cellphone.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                                    !string.IsNullOrEmpty(v.Patient.Email) && v.Patient.Email.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            }
            var totalCount = await visitQuery.CountAsync();
            var items = await visitQuery.Skip((request.PageNo - 1) * request.PageSize)
                                        .Take(request.PageSize)
                                        .Select(v => new VisitSummary
                                        {
                                            PatientId = v.PatientId,
                                            PatientName = v.Patient.Name,
                                            PatientSurname = v.Patient.Surname,
                                            PatientIdNumber = v.Patient.IdNumber,
                                            PatientCellNumber = v.Patient.Cellphone ?? "",
                                            PatientEmail = v.Patient.Email ?? "",
                                            FacilityId = v.FacilityId,
                                            AppointmentId = v.AppointmentId,
                                            VisitType = v.VisitType,
                                            ArrivalTime = v.ArrivalTime,
                                            Status = v.Status,
                                            QueueNumber = v.QueueNumber
                                        })
                                        .ToListAsync();

            return new PageData<VisitSummary?>
            {
                Data = items,
                PageNumber = request.PageNo,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }

        private IQueryable<Visit> GetVisitsByFacilityId(Guid facilityId) 
        {
            IQueryable<Visit> visitQuery = _dbSet.Where(v => v.FacilityId == facilityId && !v.IsDeleted)
                                                                 .OrderByDescending(x => x.ArrivalTime);

            return visitQuery;
        }
    }
}
