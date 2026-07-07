using DentistryAppointmentPrac.Shared;
using DentistryAppointmentPrac.Shared.RoomDetails.BusinessLogic;
using DentistryAppointmentPrac.Shared.RoomDetails.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentistryAppointmentPrac.API.Controllers
{
    [Route("room-details")]
    [Authorize(Policy ="MultiToken")]
    [ApiController]
    public class RoomDetailsController(IRoomService implementation) : BaseController<IRoomService>(implementation) 
    {
        [HttpPost("create-room")]
        public async Task<ActionResult<string>> CreateRoom(RoomDetailsRequest request)
        {
            var result = await implementation.CreateRoom(request);
            return string.IsNullOrEmpty(result) ?
                BadRequest("Failed to create room") : 
                Ok(result);
        }

        [HttpPut("update-room/{id}")]
        public async Task<ActionResult<string>> UpdateRoomDetails(Guid id,  RoomDetailsRequest request)
        {
            var result = await implementation.UpdateRoomDetails(id, request);
            return !string.IsNullOrEmpty(result) ? 
                Ok(result) : 
                BadRequest("Failed to update room details");
        }

        [HttpPut("delete-room/{id}")]
        public async Task<ActionResult<string>> DeleteRoomDetails(Guid id)
        {
            var result = await implementation.DeleteRoomDetails(id);
            return !string.IsNullOrEmpty(result) ? 
                Ok(result) : 
                BadRequest("Failed to delete room");
        }

        [HttpPut("update-available-timeslots")]
        public async Task<ActionResult<string>> UpdateAvailableTimeslot(UpdateAvailableTimeslotsRequest request)
        {
            var result = await implementation.UpdateAvailableTimeslots(request);
            return result ?
                Ok("Successfully update available timeslots") :
                BadRequest($"Cannot update available timeslots to: {request.AvailableTimeslots}");
        }
    }
}
