using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository;
using DentistryAppointmentPrac.Shared.Utilities.Imports.DTO;
using DentistryAppointmentPrac.Shared.Utilities.Imports.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.Repository
{
    public class ImportRepository<TDbContext>(TDbContext dbContext, UserContext userContext) : BaseWritableRepository<ImportProcess, ImportProcessResponse>(dbContext, userContext), IImportRepository
        where TDbContext : DbContext
    {
        public async Task<Department?> GetDepartmentAsync(Guid departmentId)
        {
            return await _dbContext.Set<Department>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == departmentId);
        }

        public async Task<ImportProcess?> GetImportToProcessAsync()
        {
            return await _dbContext.Set<ImportProcess>().IgnoreQueryFilters()
                                                        .Where(x => (x.Status == ImportStatus.Pending || x.Status == ImportStatus.Approved) && x.Status != ImportStatus.QuickImport)
                                                        .OrderBy(x => x.LastUpdated)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<PageData<ImportActivitySummary?>> GetPagedImportProcessActivityAsync(ImportProcessSummaryPagedRequest request)
        {
            IQueryable<ImportProcess> importQuery;
            importQuery = GetImportProcessByStatus(request.Status);

            importQuery = (request.StartDate != null || request.EndDate != null) ?
                          GetImportProcessByDateFilter(importQuery, request.StartDate, request.EndDate) : importQuery;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                importQuery = importQuery.Where(a => a.LastUpdated.ToString().Contains(search) || //fix nullable
                                              (a.Status.ToString().Contains(search)) ||
                                              (a.UpdatedBy!.Contains(search)) ||
                                              (a.Type.ToString().Contains(search)));
            }

            var totalEntries = await importQuery.CountAsync();

            var items = await importQuery
                              .Skip((Math.Min(1, request.PageNo) - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .Select(x => new ImportActivitySummary()
                              {
                                  Id = x.Id,
                                  Job = $"{x.Type} Import Job",
                                  Destination = x.Type.ToString(),
                                  Records = new()
                                  {
                                      FailedRecordCount = x.NumberOfErrors,
                                      SuccessRecordCount = x.NumberOfInserts + x.NumberOfUpdates,
                                      TotalRecordCount = (x.NumberOfUpdates + x.NumberOfInserts + x.NumberOfErrors) - x.NumberOfRowsRemoved
                                  },
                                  Source = x.Document.DocumentName,
                                  Status = x.Status,
                                  Timestamp = x.LastUpdated != null ? (DateTime)x.LastUpdated : (DateTime)x.CreatedDate!,
                                  Type = x.Type
                              }).ToListAsync();
            return new PageData<ImportActivitySummary?>
            {
                Data = items,
                PageNumber = request.PageNo,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalEntries / (double)request.PageSize)
            };
        }

        private IQueryable<ImportProcess> GetImportProcessByStatus(ImportStatus? status)
        {
            IQueryable<ImportProcess> importQuery;
            if (userContext.Permissions.Contains(PermissionEnum.Admin))
            {
                importQuery = status == null ? _dbSet.IgnoreQueryFilters()
                                                     .Include(x => x.Document).Where(x => x.DocumentId != Guid.Empty)
                                                     .OrderBy(x => x.Status == ImportStatus.Pending)
                                                     .ThenByDescending(x => x.LastUpdated)
                                                     :
                                               _dbSet.IgnoreQueryFilters()
                                                     .Include(x => x.Document)
                                                     .Where(x => x.Status == status && x.DocumentId != Guid.Empty)
                                                     .OrderByDescending(x => x.LastUpdated);
            }
            else
            {
                importQuery = status == null ? _dbSet.Where(x => x.DepartmentId == userContext.DentistDepartmentId && x.DocumentId != Guid.Empty)
                                                     .OrderBy(x => x.Status == ImportStatus.Pending)
                                                     .ThenByDescending(x => x.LastUpdated) :

                                               _dbSet.Where(x => x.Status == status && x.DocumentId != Guid.Empty)
                                                     .OrderByDescending(x => x.LastUpdated);
            }

            return importQuery;
        }

        private static IQueryable<ImportProcess> GetImportProcessByDateFilter(IQueryable<ImportProcess> importQuery, DateTime? startDate, DateTime? endDate)
        {
            if(startDate != null && endDate != null)
            {
                return importQuery.Where(x => x.LastUpdated >= startDate.Value && x.LastUpdated <= endDate.Value);
            }

            if(startDate == null && endDate != null)
            {
                return importQuery.Where(x => x.LastUpdated <= endDate.Value);
            }
            
            if(endDate == null && startDate != null)
            {
                return importQuery.Where(x => x.LastUpdated > startDate.Value && x.LastUpdated <= DateTime.UtcNow);
            }
            return importQuery;
        }
    }
}
