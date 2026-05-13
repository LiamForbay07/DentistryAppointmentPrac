using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Department : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        //Review these collections:
        public ICollection<RoomDetails> Rooms { get; set; } = [];
        public virtual ICollection<Dentist> Dentists { get; set; } = [];
    }
}
