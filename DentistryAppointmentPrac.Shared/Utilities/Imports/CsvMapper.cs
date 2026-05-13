using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports
{
    public class CsvMapper<T> : ClassMap<T> where T : class
    {
        public CsvMapper(Dictionary<string, string> map)
        {
            foreach(var key in map)
            {
                try
                {
                    var propertyName = (key.Key ?? string.Empty).Trim();
                    var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (property == null)
                        continue;

                    var columnName = (key.Value ?? string.Empty).Trim();
                    Map(typeof(T), property).Name(columnName);
                }
                catch 
                {
                    continue;
                }
            }
        }

        public CsvMapper(IEnumerable<string>? requestedColumns)
        {
            var properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var selectedProperties = (requestedColumns == null || !requestedColumns.Any())
                ? properties
                : properties.Where(p => requestedColumns
                    .Any(c => c.Equals(p.Name, StringComparison.OrdinalIgnoreCase)));

            foreach(var prop in selectedProperties)
            {
                Map(typeof(T), prop).Name(prop.Name.Replace("_", " "));
            }
        }
    }
}
