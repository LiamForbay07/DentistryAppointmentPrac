using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Department : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        //Review these collections:
        public ICollection<RoomDetail> Rooms { get; set; } = [];
        public ICollection<Dentist> Dentists { get; set; } = [];
    }
}
