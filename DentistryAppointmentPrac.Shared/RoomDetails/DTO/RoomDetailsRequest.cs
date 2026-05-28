using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.DTO
{
    public class RoomDetailsRequest : IRequestHandler<RoomDetail, RoomDetailsRequest>
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<string> AvailableTimes { get; set; } = []; //Create a builder for available times

        public static RoomDetail ToEntity(RoomDetailsRequest request)
        {
            if (request == null) return new();
            return new RoomDetail
            {
                Id = Guid.NewGuid(),
                AvailableTimes = request.AvailableTimes,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                LastUpdated = DateTime.UtcNow,
                Location = request.Location,
                Name = request.Name
            };
        }
    }
}
