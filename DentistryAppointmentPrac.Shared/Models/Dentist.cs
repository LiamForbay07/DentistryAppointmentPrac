using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Dentist : BaseModel
    {
        public Guid UserId { get; set; }
        public DentalSpecializationEnum Specialization { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public string EmployeeIdNumber { get; set; } = string.Empty;

        public User? User { get; set; }
        
    }

    public enum DentalSpecializationEnum
    {
        General,
        Surgery
    }
}
