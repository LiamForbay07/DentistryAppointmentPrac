using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;

namespace DentistryAppointmentPrac.Shared.Models.DbConfigurations
{
    public class UserDbConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            var permissionsConverter = new ValueConverter<List<PermissionEnum>, string>(
                v => JsonSerializer.Serialize(v.Select(p => p.ToString()), (JsonSerializerOptions)null!),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!)!
                        .Select(s => Enum.Parse<PermissionEnum>(s))
                        .ToList()
            );

            builder.Property(x => x.UserType).HasConversion<string>();
            builder.Property(x => x.Permissions).HasConversion(permissionsConverter);
        }
    }
}
