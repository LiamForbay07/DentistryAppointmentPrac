using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Ledger.BusinessLogic
{
    public interface ILegderService
    {
        Task RecordTransaction(Guid from, Guid to, decimal amount);
    }
}
