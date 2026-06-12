using DentistryAppointmentPrac.Shared.Ledger.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace DentistryAppointmentPrac.Shared.Ledger.Repository
{
    public class LedgerRepository<TContext>(TContext dbContext, UserContext userContext) : BaseWritableRepository<LedgerEntry, LedgerEntrySummaryResponse>(dbContext, userContext), ILedgerRepository
        where TContext : DbContext
    {
        public DbContext GetDbContext() => dbContext;
    }
}
