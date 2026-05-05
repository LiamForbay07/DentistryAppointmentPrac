using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces
{
    public class BaseWriteableImplementation<T, TResponse, TSummary, TRequest>(IBaseWriteableRepository<T, TSummary> repository) : BaseReadableImplementation<T, TResponse, TSummary>(repository), IWriteableImplementation<T, TResponse, TSummary, TRequest>
        where T : BaseModel
        where TResponse : class, IResponseHandler<T, TResponse>
        where TSummary : class, ISummaryHandler<T, TSummary>
        where TRequest : class, IRequestHandler<T, TRequest>
    {
        public virtual async Task<string> DefaultCreateAsync(TRequest request, string? searchReduce)
        {
            var model = TRequest.ToEntity(request);
            if (model != null)
            {
                repository.AddItemAsync(model);
                await repository.ApplySaveChangesAsync();
                return model.Id.ToString();
            }
            else
                throw new Exception("Something went wrong");
        }

        public virtual async Task<TResponse?> DefaultUpdateAsync(Guid id, TRequest request, string? searchReduce)
        {
            var model = await repository.GetTrackedAsync(id);
            if (model == null)
            {
                throw new Exception("Field not found");
            }

            model.CurrentValues.SetValues(request);
            await repository.ApplySaveChangesAsync();
            return TResponse.FromEntity(model.Entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                await repository.DeleteItemAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
