using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Dentist : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid FacilityId { get; set; }
        public string HPCSANumber { get; set; } = string.Empty;
        public string Speciality { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public User? User { get; set; }
        
    }

    public enum DentalSpecializationEnum
    {
        General,
        Surgery
    }
}
