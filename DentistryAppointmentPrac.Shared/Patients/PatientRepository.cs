using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Patients.DTO;
using DentistryAppointmentPrac.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace DentistryAppointmentPrac.Shared.Patients
{
    public class PatientRepository<T>(T dbContext, UserContext userContext)
        : BaseWritableRepository<Patient, PatientSummaryResponse>(dbContext, userContext), IPatientRepository where T : DbContext
    {
        public async Task<PageData<PatientSummaryResponse?>> GetPatientsBySearch(GetPatientSummaryPagedRequest request)
        {
            IQueryable<Patient> patientQuery = dbContext.Set<Patient>();
            patientQuery = (request.StartDate != null || request.EndDate != null) ?
                GetPatientByDateRanges(patientQuery, request.StartDate, request.EndDate) : patientQuery;

            if(!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                patientQuery = patientQuery.Where(p => p.Name.Contains(search) || 
                                         p.Surname.Contains(search) || 
                                         !string.IsNullOrEmpty(p.Email) && p.Email.Contains(search) ||
                                         !string.IsNullOrEmpty(p.MedicalAidNo) && p.MedicalAidNo.Contains(search));
            }

            var totalEntries = await patientQuery.CountAsync();

            var items = await patientQuery.Skip((request.PageNo - 1) * request.PageSize)
                                        .Take(request.PageSize)
                                        .Select(x => new PatientSummaryResponse()
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Surname = x.Surname,
                                            Email = x.Email,
                                            MedicalAidNo = x.MedicalAidNo,
                                            Cellphone = x.Cellphone,
                                            DateOfBirth = x.DateOfBirth,
                                            Gender = x.Gender,
                                            IdNumber = x.IdNumber
                                        }).ToListAsync();

            return new PageData<PatientSummaryResponse?>
            {
                Data = items,
                PageNumber = request.PageNo,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalEntries / (double)request.PageSize)
            };
        }

        private static IQueryable<Patient> GetPatientByDateRanges(IQueryable<Patient> query, DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null && endDate != null)
            {
                query = query.Where(p => p.DateOfBirth >= startDate && p.DateOfBirth <= endDate);
            }
            else if (startDate != null)
            {
                query = query.Where(p => p.DateOfBirth >= startDate);
            }
            else if (endDate != null)
            {
                query = query.Where(p => p.DateOfBirth <= endDate);
            }
            return query;
        }
    }
}
