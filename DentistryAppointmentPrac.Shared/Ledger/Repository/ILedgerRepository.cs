using DentistryAppointmentPrac.Shared.Ledger.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Ledger.Repository
{
    public interface ILedgerRepository : IBaseWriteableRepository<LedgerEntry, LedgerEntrySummaryResponse>
    {
        DbContext GetDbContext();
    }
}
