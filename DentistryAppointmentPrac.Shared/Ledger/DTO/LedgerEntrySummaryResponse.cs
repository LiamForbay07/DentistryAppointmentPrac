using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Ledger.DTO
{
    public class LedgerEntrySummaryResponse : ISummaryHandler<LedgerEntry, LedgerEntrySummaryResponse>, IResponseHandler<LedgerEntry, LedgerEntrySummaryResponse>
    {
        public static Expression<Func<LedgerEntry, LedgerEntrySummaryResponse>> SummaryMap => throw new NotImplementedException();

        public static LedgerEntrySummaryResponse? FromEntity(LedgerEntry entity)
        {
            throw new NotImplementedException();
        }
    }
}
