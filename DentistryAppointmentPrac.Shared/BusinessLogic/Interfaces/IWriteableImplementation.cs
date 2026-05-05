using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces
{
    public interface IWriteableImplementation<T, TResponse, ISummary, TRequest> : IImplementation<T, TResponse, ISummary>
 where T : BaseModel
 where TResponse : class, IResponseHandler<T, TResponse>
 where ISummary : class, ISummaryHandler<T, ISummary>
 where TRequest : class, IRequestHandler<T, TRequest>
    {
        Task<bool> DeleteAsync(Guid id);
        Task<TResponse?> DefaultUpdateAsync(Guid id, TRequest request, string? searchReduce);
        Task<string> DefaultCreateAsync(TRequest request, string? searchReduce);
    }
}
