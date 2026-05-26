using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.DTO
{
    public class RoomDetailsRequest : IRequestHandler<RoomDetail, RoomDetailsRequest>
    {
        public static RoomDetail ToEntity(RoomDetailsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
