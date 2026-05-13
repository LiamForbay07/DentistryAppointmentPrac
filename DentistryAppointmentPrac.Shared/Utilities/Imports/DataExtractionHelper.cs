using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports
{
    public static class DataExtractionHelper
    {
        public static IEnumerable<List<T>> ExtractCsv<T>(Stream? stream, Dictionary<string, string> mapping, char? delimiter, int batchSize = 200) where T : class, new()
        {
            if (stream == null) yield break;
            using var reader = new StreamReader(stream);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = delimiter == null,
                HasHeaderRecord = true,
                PrepareHeaderForMatch = header => header.Header.Trim(),
                HeaderValidated = null,
                MissingFieldFound = null
            };
            using var csv = new CsvReader(reader, config);

            csv.Context.RegisterClassMap(new CsvMapper<T>(mapping));
            var batch = new List<T>(batchSize);

            foreach(var record in csv.GetRecords<T>())
            {
                batch.Add(record);
                if(batch.Count == batchSize)
                {
                    yield return batch;
                    batch = new List<T>(batchSize);
                }
            }

            if (batch.Count > 0)
                yield return batch;
        }

        public static IEnumerable<List<T>> ExtractExcel<T>(Stream? stream,Dictionary<string, string> mapping,int batchSize = 200) where T : new()
        {
            var records = new List<T>(batchSize);

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);
            if (worksheet == null) yield break;

            var usedRange = worksheet.RangeUsed();
            if (usedRange == null) yield break;

            var rows = usedRange.RowsUsed();
            var headerRow = rows.First();

            var headers = headerRow.Cells()
                .Select((c, index) => new
                {
                    Name = c.GetString().Trim(),
                    Index = index + 1
                })
                .ToDictionary(x => x.Name, x => x.Index);

            var columnMap = new Dictionary<PropertyInfo, int>();

            foreach (var map in mapping)
            {
                if (!headers.TryGetValue(map.Value, out var columnIndex))
                    continue;

                var property = typeof(T).GetProperty(map.Key);
                if (property == null)
                    continue;

                columnMap[property] = columnIndex;
            }

            foreach (var row in rows.Skip(1))
            {
                var entity = new T();

                foreach (var map in columnMap)
                {
                    var property = map.Key;
                    var cell = row.Cell(map.Value);

                    var value = ConvertToType(cell.GetValue<string>(), property.PropertyType);
                    property.SetValue(entity, value);
                }

                records.Add(entity);

                if (records.Count >= batchSize)
                {
                    yield return records;
                    records = new List<T>(batchSize);
                }
            }

            if (records.Count > 0)
                yield return records;
        }

        private static object? ConvertToType(string value, Type type)
        {
            if (type == typeof(object))
                return value;

            var targetType = Nullable.GetUnderlyingType(type) ?? type;

            if (!string.IsNullOrEmpty(value))
                return type.IsValueType ? Activator.CreateInstance(type) : null;

            var cleaned = value.Trim();

            if (targetType == typeof(Guid))
                return Guid.Parse(cleaned);

            if (targetType.IsEnum)
                return Enum.Parse(targetType, cleaned, true);

            return Convert.ChangeType(cleaned, targetType, CultureInfo.InvariantCulture);
        }
    }
}
