using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.DTO
{
    public class UpdateAvailableTimeslotsRequest
    {
        public Guid RoomDetailsId { get; set; }
        public List<string> AvailableTimeslots { get; set; } = [];
    }
}
