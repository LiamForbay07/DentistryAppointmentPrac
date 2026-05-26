using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DentalAppointments.DTO
{
    public class DentalAppointmentRequest : IRequestHandler<DentalAppointment, DentalAppointmentRequest>
    {
        public Guid DentistId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomDetailsId { get; set; }
        public string AppointmentTimeslot { get; set; } = string.Empty;
        public string AppointmentDate { get; set; } = string.Empty;
        public bool IsAppointmentConfirmed { get; set; }

        public static DentalAppointment ToEntity(DentalAppointmentRequest request)
        {
            if (request == null) return new();

            return new DentalAppointment
            {
                DentistId = request.DentistId,
                UserId = request.UserId,
                RoomDetailsId = request.RoomDetailsId,
                AppointmentTimeslot = request.AppointmentTimeslot,
                IsAppointmentConfirmed = request.IsAppointmentConfirmed,
                AppointmentDate = request.AppointmentDate,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                LastUpdated = DateTime.UtcNow
            };
        }
    }
}
