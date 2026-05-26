using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using DentistryAppointmentPrac.Shared.RoomDetails.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.Repository
{
    public interface IRoomRepository : IBaseWriteableRepository<RoomDetail,RoomDetailResponseSummary>
    {
    }
}
