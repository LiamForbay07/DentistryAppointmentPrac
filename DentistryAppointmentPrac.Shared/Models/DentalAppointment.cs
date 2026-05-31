using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class DentalAppointment : BaseModel
    {
        public Guid DentistId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomDetailsId { get; set; }
        public virtual RoomDetail? RoomDetails { get; set; } 
        public string AppointmentTimeslot { get; set; } = string.Empty;
        public string AppointmentDate { get; set; } = string.Empty;
        public bool IsAppointmentConfirmed { get; set; }
    }
}
