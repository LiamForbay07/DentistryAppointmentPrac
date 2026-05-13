using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Dentist : BaseModel
    {
        public Guid UserId { get; set; }
        public List<DentalSpecializationEnum> Specialization { get; set; } = [];
        public User? User { get; set; }
        
    }

    public enum DentalSpecializationEnum
    {
        General,
        Surgery
    }
}
