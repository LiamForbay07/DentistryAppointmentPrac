using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.RoomDetails.DTO;
using DentistryAppointmentPrac.Shared.RoomDetails.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.BusinessLogic
{
    public class RoomService(IRoomRepository repository) : BaseWriteableImplementation<RoomDetail, RoomDetailResponseSummary, RoomDetailResponseSummary, RoomDetailsRequest>(repository), IRoomService
    {
        public Task<string> CreateRoom(RoomDetailsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteRoomDetails(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAvailableTimeslots(UpdateAvailableTimeslotsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateRoomDetails(Guid id, RoomDetailsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
