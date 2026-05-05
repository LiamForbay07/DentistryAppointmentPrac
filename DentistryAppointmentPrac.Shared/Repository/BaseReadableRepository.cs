using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DentistryAppointmentPrac.Shared.Repository
{
    public class BaseReadableRepository<T, TSummary>(DbContext dbContext) : IBaseReadableRepository<T, TSummary>
        where T : BaseModel
        where TSummary : class
    {
        protected readonly DbContext _dbContext = dbContext;
        protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

        public async Task<T?> GetItemByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null)
        {
            IQueryable<T> query = _dbSet;
            if (queryable != null)
                query = queryable(query);

            var result = await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            return result;
        }

        public async Task<T?> GetTrackedItemByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? queryable = null)
        {
            IQueryable<T> query = _dbSet;
            if (queryable != null)
                query = queryable(query);

            var result = await query.FirstOrDefaultAsync(e => e.Id == id);
            return result;
        }

        public async Task<EntityEntry<T>?> GetTrackedAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsTracking();

            foreach (var include in includes)
                query = query.Include(include);
            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            return entity is null
                ? null
                : _dbContext.Entry(entity);
        }

        public async Task<IEnumerable<T>> FindByFilterAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByFilterAsyncWithIgnoreQueryFilters(Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includes, bool ignoreQueryFilters)
        {
            IQueryable<T> query = _dbSet;
            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> FindSingleItemByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<PageData<TSummary>> GetPagedItemsAsync(int pageNo, int pageSize, string search, Expression<Func<T, TSummary>> summaryMap, Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.UpdatedBy.Contains(search));
            }

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            var totalEntries = await query.CountAsync();
            var entries = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(summaryMap)
                .ToListAsync();

            return new PageData<TSummary>
            {
                Data = entries,
                PageNumber = pageNo,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalEntries / (double)pageSize),
            };
        }

        public async Task<IEnumerable<TSummary>> GetAllAsync(Expression<Func<T, TSummary>> summaryMap)
        {
            return await _dbSet.Select(summaryMap).ToListAsync();
        }

        public async Task<List<T>> GetAllFullAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<PageData<TSummary>> GetPagedItemsByQueryAsync(int pageNo, int pageSize, string search, Expression<Func<T, TSummary>> summaryMap, Func<IQueryable<T>, IQueryable<T>>? queryable = null)
        {

            IQueryable<T> query = _dbSet;
            if (queryable != null)
                query = queryable(query);

            var totalEntries = await query.CountAsync();
            var entries = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(summaryMap)
                .ToListAsync();

            return new PageData<TSummary>
            {
                Data = entries,
                PageNumber = pageNo,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalEntries / (double)pageSize),
            };
        }
    }
}
