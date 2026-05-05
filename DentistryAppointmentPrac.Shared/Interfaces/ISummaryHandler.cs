using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Interfaces
{
    public interface ISummaryHandler<T, TSummary>
            where T : BaseModel
            where TSummary : class
    {
        static abstract Expression<Func<T, TSummary>> SummaryMap { get; }
    }
}
