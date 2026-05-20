using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Repository.Interfaces
{
    public interface IBaseWriteableRepository<T, TSummary> : IBaseReadableRepository<T, TSummary>
       where T : BaseModel
       where TSummary : class, ISummaryHandler<T, TSummary>
    {
        void AddItemAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<bool> DeleteItemAsync(Guid id);
        void UpdateItemAsync(T entity);
        void UpdateRangeItems(params T[] entities);
        Task<int> ApplySaveChangesAsync();
        Task<T> ExecuteInTransactionAsync(Func<Task<T>> action);
    }
}
