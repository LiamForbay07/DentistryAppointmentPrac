using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports
{
    public class CsvImporter<T> where T : class, new() //For offline files
    {
        private readonly MemberInfo[] _members;
        public CsvImporter(MemberInfo[] members)
        {
            _members = members;
        }

        public IEnumerable<T> Import(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                DetectDelimiterValues = ["\t", ",", ";"],
                Encoding = Encoding.UTF8
            });

            var records = new List<T>();

            while (csv.Read())
            {
                var entity = new T();
                foreach(var member in _members)
                {
                    //Check for attributes
                    var attr = member.GetCustomAttribute<CsvColumnAttribute>();
                    var headerName = attr?.HeaderName ?? member.Name;

                    if (!csv.TryGetField(headerName, out string? rawValue)) continue;

                    object? converted = CsvImporter<T>.ConvertValue(member, rawValue ?? "");

                    switch (member)
                    {
                        case PropertyInfo p:
                            p.SetValue(entity, converted);
                            break;
                        case FieldInfo f:
                            f.SetValue(entity, converted);
                            break;
                    }

                }
                records.Add(entity);
            }
            return records;
        }

        private static object? ConvertValue(MemberInfo member, string raw)
        {
            Type type = member switch
            {
                PropertyInfo p => Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType,
                FieldInfo f => Nullable.GetUnderlyingType(f.FieldType) ?? f.FieldType,
                _ => typeof(string)
            };

            if (string.IsNullOrEmpty(raw)) return null;
            return Convert.ChangeType(raw, type);
        }
    }
}
