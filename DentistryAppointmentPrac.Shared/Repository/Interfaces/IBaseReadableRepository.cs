using DentistryAppointmentPrac.Shared.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Repository.Interfaces
{
    public interface IBaseReadableRepository<T, TSummary>
        where T : class
        where TSummary : class
    {
        Task<T?> GetItemByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null);
        Task<T?> GetTrackedItemByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null);
        Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[]? includes);
        Task<IEnumerable<T>> FindByFilterAsyncWithIgnoreQueryFilters(Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includes, bool ignoreQueryFilters);
        Task<T?> FindSingleItemByFilterAsync(Expression<Func<T, bool>> filter);
        Task<PageData<TSummary>> GetPagedItemsAsync(int pageNo, int pageSize, string search, Expression<Func<T, TSummary>> summaryMap, Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includes);
        Task<PageData<TSummary>> GetPagedItemsByQueryAsync(int pageNo, int pageSize, string search, Expression<Func<T, TSummary>> summaryMap, Func<IQueryable<T>, IQueryable<T>>? queryable = null);
        Task<EntityEntry<T>?> GetTrackedAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<TSummary>> GetAllAsync(Expression<Func<T, TSummary>> summaryMap);
        Task<List<T>> GetAllFullAsync();
    }
}
