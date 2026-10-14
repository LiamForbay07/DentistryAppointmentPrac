using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Appointment : BaseModel
    {
        public Guid PatientId { get; set; }
        public Guid DentistId { get; set; }
        public Guid FacilityId { get; set;  }
        public Guid RoomId { get; set; } //instead of chair

        public DateTime AppointmentDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid AppointmentStatusId { get; set; }
        public AppointmentTypes AppointmentType { get; set; }
        public bool IsBookedAppointment { get; set; } = false;
        public bool IsWalkInAppointment { get; set; } = false;
    }
}
