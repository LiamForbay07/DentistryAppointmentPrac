using DentistryAppointmentPrac.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentistryAppointmentPrac.Payment.Api.Models.DbConfigurations
{
    public class IdempotencyRecordConfigurations : IEntityTypeConfiguration<IdempotencyRecord>
    {
        public void Configure(EntityTypeBuilder<IdempotencyRecord> builder)
        {
            builder.HasIndex(x => new { x.Id, x.UserId, x.Endpoint })
                   .IsUnique()
                   .HasDatabaseName("IX_IdempotencyRecord_Key_User");
        }
    }
}
