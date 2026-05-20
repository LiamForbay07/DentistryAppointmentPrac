using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.Mappers
{
    public class UserMapper
    {
        public string? Display_Name { get; set; }
        public string? Email_Address { get; set; }
        public string? Title { get; set; }
        public string? User_Type { get; set; }
        public string? ID_Number { get; set; }
        public string? Mobile_Number { get; set; }
    }
}
