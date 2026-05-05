using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.BusinessLogic
{
    public class BaseReadableImplementation<T, TResponse, TSummary>(IBaseReadableRepository<T, TSummary> repository) : IImplementation<T, TResponse, TSummary>
        where T : BaseModel
        where TResponse : class, IResponseHandler<T, TResponse>
        where TSummary : class, ISummaryHandler<T, TSummary>
    {
        public async Task<IEnumerable<TSummary>> GetAll()
        {
            return await repository.GetAllAsync(TSummary.SummaryMap);
        }

        public async Task<TResponse?> GetItemByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null)
        {
            var response = await repository.GetItemByIdAsync(id, queryable);
            return TResponse.FromEntity(response);
        }

        public async Task<TResponse?> GetItemByIdAsyncWithInclude(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null)
        {
            var response = await repository.GetTrackedItemByIdAsync(id, queryable);
            return TResponse.FromEntity(response);
        }

        public async Task<PageData<TSummary>> GetPagedItemAsyncWithInclude(int pageNo, int pageSize, string search, Expression<Func<T, bool>>? filter, params Expression<Func<T, object>>[] includes)
        {
            return await repository.GetPagedItemsAsync(pageNo, pageSize, search, TSummary.SummaryMap, filter, includes);
        }

        public async Task<PageData<TSummary>> GetPagedItemsAsync(int pageNo, int pageSize, string search)
        {
            return await repository.GetPagedItemsAsync(pageNo, pageSize, search, TSummary.SummaryMap, null, null);
        }
    }
}
