using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using DentistryAppointmentPrac.Shared.RoomDetails.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.BusinessLogic
{
    public interface IRoomService : IImplementation<RoomDetail, RoomDetailResponseSummary, RoomDetailResponseSummary>
    {
        Task<string> CreateRoom(RoomDetailsRequest request);
        Task<string> UpdateRoomDetails(Guid id, RoomDetailsRequest request);
        Task<string> DeleteRoomDetails(Guid id);
        Task<bool> UpdateAvailableTimeslots(UpdateAvailableTimeslotsRequest request);
    }
}
