using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.RoomDetails.DTO;
using DentistryAppointmentPrac.Shared.RoomDetails.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.BusinessLogic
{
    public class RoomService(IRoomRepository repository, UserContext userContext) : BaseWriteableImplementation<RoomDetail, RoomDetailResponseSummary, RoomDetailResponseSummary, RoomDetailsRequest>(repository), IRoomService
    {
        public async Task<string> CreateRoom(RoomDetailsRequest request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));

            var model = RoomDetailsRequest.ToEntity(request);
            model.UpdatedBy = userContext.Username;
            model.UpdatedById = Guid.Parse(userContext.UserId);
            model.LastUpdated = DateTime.UtcNow;

            repository.AddItemAsync(model);

            return await repository.ApplySaveChangesAsync() > 0 ? model.Id.ToString() : string.Empty;
        }

        public async Task<string> DeleteRoomDetails(Guid id)
        {
            bool result = false;
            if(id == Guid.Empty) return string.Empty;

            var roomDetails = await repository.GetTrackedItemByIdAsync(id);
            if(roomDetails != null)
            {
                result = await repository.DeleteItemAsync(roomDetails.Id);
            }
            return result ? "Successfully deleted" : "Failed to delete";
        }

        public async Task<bool> UpdateAvailableTimeslots(UpdateAvailableTimeslotsRequest request)
        {
            bool result = false;
            ArgumentNullException.ThrowIfNull(request);

            var model = await repository.GetTrackedItemByIdAsync(request.RoomDetailsId, null);
            ArgumentNullException.ThrowIfNull(model);

            model.AvailableTimes = request.AvailableTimeslots;
            repository.UpdateItemAsync(model);

            result = await repository.ApplySaveChangesAsync() > 0;
            return result;
        }

        public async Task<string> UpdateRoomDetails(Guid id, RoomDetailsRequest request)
        {
            if (id == Guid.Empty) throw new InvalidDataException("Invaild Room");

            var model = await repository.GetTrackedItemByIdAsync(id, null);
            ArgumentNullException.ThrowIfNull(model);

            model.AvailableTimes = request.AvailableTimes;
            model.Name = request.Name;
            model.Location = request.Location;
            model.UpdatedBy = userContext.Username;
            model.UpdatedById = Guid.Parse(userContext.UserId);
            model.LastUpdated = DateTime.UtcNow;

            repository.UpdateItemAsync(model);
            var result = await repository.ApplySaveChangesAsync() > 0;
            return result ? "Successfully updated room details" : "Failed to update room details";
        }
    }
}
