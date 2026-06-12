using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Ledger.DTO
{
    public class LedgerRequest : IRequestHandler<LedgerEntry, LedgerRequest>
    {
        public static LedgerEntry ToEntity(LedgerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
