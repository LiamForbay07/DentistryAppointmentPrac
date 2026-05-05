using DentistryAppointmentPrac.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces
{
    public interface IImplementation<T, TResponse, TSummary> : IBaseImplementation
    {
        Task<TResponse?> GetItemByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null);
        Task<TResponse?> GetItemByIdAsyncWithInclude(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null);
        Task<PageData<TSummary>> GetPagedItemsAsync(int pageNo, int pageSize, string search);
        Task<IEnumerable<TSummary>> GetAll();
        Task<PageData<TSummary>> GetPagedItemAsyncWithInclude(int pageNo, int pageSize, string search, Expression<Func<T, bool>>? filter, params Expression<Func<T, object>>[] includes);
    }
}
