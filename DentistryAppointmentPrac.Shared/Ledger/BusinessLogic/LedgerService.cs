using DentistryAppointmentPrac.Shared.Ledger.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Ledger.BusinessLogic
{
    public class LedgerService(ILedgerRepository repository) : ILegderService
    {
        public async Task RecordTransaction(Guid from, Guid to, decimal amount)
        {
            var db = repository.GetDbContext();
            using var tx = await db.Database.BeginTransactionAsync();

            repository.AddItemAsync(new()
            {
                AccountId = from,
                Amount = -amount,
                ReferenceId = Guid.NewGuid()
            });

            repository.AddItemAsync(new()
            {
                AccountId = to,
                Amount = amount,
                ReferenceId = Guid.NewGuid()
            });

            await repository.ApplySaveChangesAsync();
            await tx.CommitAsync();
        }
    }
}
