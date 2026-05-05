using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Repository
{
    public abstract class BaseWritableRepository<T, TSummary>(DbContext dbContext, UserContext userContext) : BaseReadableRepository<T, TSummary>(dbContext), IBaseWriteableRepository<T, TSummary>
        where T : BaseModel
        where TSummary : class, ISummaryHandler<T, TSummary>
    {
        public void UpdateItemAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var entity = await GetTrackedItemByIdAsync(id);

            if (entity != null)
            {
                entity.IsDeleted = false;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Model not found");
            }
        }

        public void AddItemAsync(T entity)
        {
            _dbSet.Add(entity);
        }

        public void UpdateRangeItems(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task<int> ApplySaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<T> ExecuteInTransactionAsync(Func<Task<T>> action)
        {
            await using var tx = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await action();
                await _dbContext.SaveChangesAsync();
                await tx.CommitAsync();

                return result;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
    }
}
