using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ClosedXML.Attributes;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Delegates;
using DentistryAppointmentPrac.Shared.Utilities;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class User : BaseModel
    {
        [CsvColumn("Display Name")]
        public string DisplayName { get; set; } = string.Empty;
        [CsvColumn("Title")]
        public string Title { get; set; } = string.Empty;
        [CsvColumn("First Name")]
        public string Name { get; set; } = string.Empty;
        [CsvColumn("Surname")]
        public string Surname { get; set; } = string.Empty;
        [CsvColumn("Username")]
        public string Username { get; set; } = string.Empty;
        [CsvColumn("Permissions")]
        public List<PermissionEnum> Permissions { get; set; } = [];
        [CsvColumn("User Type")]
        public UserType UserType { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime? PasswordExpiryDate { get; set; }
        [CsvColumn("ID Number")]
        public string IDNumber { get; set; } = string.Empty;
        public virtual Dentist? Dentist { get; set; }
    }

    public enum UserType
    {
        Patient,
        Dentist,
        DentalAssistant,
    }

    public enum PermissionEnum
    {
        //View,
        //Edit,
        //Create,
        //Delete
        Dentist,
        Admin,
        Patient,
        TechSupport,
        DentalAssistant
    }
}
