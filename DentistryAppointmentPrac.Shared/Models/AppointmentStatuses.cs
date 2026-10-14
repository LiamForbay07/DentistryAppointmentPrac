using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class AppointmentStatuses : BaseModel
    {
        public AppointmentStages Description { get; set; } //booked/arrived/waiting/called/in treatment/completed/cancelled/no show
    }
}
