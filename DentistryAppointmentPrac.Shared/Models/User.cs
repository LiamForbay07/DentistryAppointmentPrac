using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class User : BaseModel
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<PermissionEnum> Permissions { get; set; } = [];
        public UserType UserType { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime? PasswordExpiryDate { get; set; }
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
