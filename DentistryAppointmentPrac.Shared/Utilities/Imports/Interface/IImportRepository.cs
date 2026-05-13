using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities.Imports.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.Interface
{
    public interface IImportRepository : IBaseWriteableRepository<ImportProcess, ImportProcessResponse>
    {
        Task<ImportProcess?> GetImportToProcessAsync();
        Task<Department?> GetDepartmentAsync(Guid departmentId);
        Task<PageData<ImportActivitySummary?>> GetPagedImportProcessActivityAsync(ImportProcessSummaryPagedRequest request);
    }
}
