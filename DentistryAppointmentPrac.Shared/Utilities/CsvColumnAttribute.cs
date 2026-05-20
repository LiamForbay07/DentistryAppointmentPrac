using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CsvColumnAttribute(string headerName) : Attribute
    {
        public string HeaderName { get; set; } = headerName;
    }
}
