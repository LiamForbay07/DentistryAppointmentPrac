using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DentistryAppointmentPrac.Shared.Models.DbConfigurations
{
    public class ImportProcessDbConfigurations : IEntityTypeConfiguration<ImportProcess>
    {
        public void Configure(EntityTypeBuilder<ImportProcess> builder)
        {
            var listComparer = new ValueComparer<List<string>>(
                 (a, b) => a!.SequenceEqual(b!),
                 a => a.Aggregate(0, (hash, v) => HashCode.Combine(hash, v)),
                 a => a.ToList()
            );

            var dictComparer = new ValueComparer<Dictionary<string, string>>(
                (d1, d2) => d1 != null && d2 != null && d1.Count == d2.Count &&
                            !d1.Except(d2).Any(),
                d => d.Aggregate(0, (a, p) => HashCode.Combine(a, p.Key.GetHashCode(), p.Value.GetHashCode())),
                d => new Dictionary<string, string>(d)
            );

            var listToJson = new ValueConverter<List<string>, string>(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                       v => JsonSerializer.Deserialize<List<string>>(v) ?? new List<string>()
                   );

            var dictToJson = new ValueConverter<Dictionary<string, string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v) ?? new Dictionary<string, string>()
            );

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };


            builder.Property(x => x.ImportChanges).HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<List<ImportChanges>>(v, jsonOptions) ?? new List<ImportChanges>()
            ).HasColumnType("nvarchar(max)");

            var comparer = new ValueComparer<List<ImportChanges>>(
                (a, b) => JsonSerializer.Serialize(a, jsonOptions) == JsonSerializer.Serialize(b, jsonOptions),
                a => a == null ? 0 : JsonSerializer.Serialize(a, jsonOptions).GetHashCode(),
                a => a == null ? new List<ImportChanges>() : JsonSerializer.Deserialize<List<ImportChanges>>(JsonSerializer.Serialize(a, jsonOptions), jsonOptions)!
            );

            builder.Property(x => x.ImportChanges).Metadata.SetValueComparer(comparer);

            builder.Property(x => x.Errors)
                      .HasConversion(listToJson!)
                      .Metadata.SetValueComparer(listComparer);
            builder.Property(x => x.Mapping)
                      .HasConversion(dictToJson)
                      .Metadata.SetValueComparer(dictComparer);

            builder.Property(x => x.Status).HasConversion<string>();
        }
    }
}
