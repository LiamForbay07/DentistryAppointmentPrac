using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Interfaces
{
    public interface IRequestHandler<T, TRequest>
      where T : BaseModel
      where TRequest : class
    {
        static abstract T ToEntity(TRequest request);
    }
}
