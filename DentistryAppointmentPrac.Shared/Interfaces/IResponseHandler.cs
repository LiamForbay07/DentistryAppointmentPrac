using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Interfaces
{
    public interface IResponseHandler<T, TResponse>
        where T : BaseModel
        where TResponse : class
    {
        static abstract TResponse? FromEntity(T entity);
    }
}
