using DentistryAppointmentPrac.Payment.Api.Models;
using DentistryAppointmentPrac.Payment.Api.Models.DbConfigurations;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Models.DbConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DentistryAppointmentPrac.Payment.Api.Utilities.Database
{
    public class PaymentAppointmentContext : DbContext
    {
        public PaymentAppointmentContext(DbContextOptions<PaymentAppointmentContext> options) : base(options)
        {
        }

        public DbSet<Payments> Payments { get; set; }
        public DbSet<LedgerEntry> LedgerEntries { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<IdempotencyRecord> IdempotencyRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentAppointmentContext).Assembly);
            modelBuilder.ApplyConfiguration(new IdempotencyRecordConfigurations());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
            base.ConfigureConventions(configurationBuilder); //Add DbConfigurations here
        }
    }
}
