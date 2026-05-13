using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports
{
    public static class CsvTemplateGenerator
    {
        public static MemoryStream GenerateCsv<T>()
        {
            var stream = new MemoryStream();
            var bom = Encoding.UTF8.GetPreamble();
            stream.Write(bom, 0, bom.Length);
            using var writer = new StreamWriter(stream, new UTF8Encoding(true), leaveOpen: true);
            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            writer.WriteLine(string.Join(";", properties.Select(p => p.Name.Replace("_", "")))); //Can use "," as well
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
