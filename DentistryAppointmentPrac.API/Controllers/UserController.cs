using DentistryAppointmentPrac.Shared;
using DentistryAppointmentPrac.Shared.Users.DTO;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities.Imports.DTO;
using DocumentFormat.OpenXml.Office2010.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentistryAppointmentPrac.API.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize(Policy = "MultiToken")]
    public class UserController(IUserService implementation) : BaseController<IUserService>(implementation)
    {
        [HttpPost("create-user")]
        public async Task<ActionResult<string>> CreateUser(UserRequest request)
        {
            var result = await implementation.CreateUser(request);
            return !string.IsNullOrEmpty(result) ? 
                Ok(result) : 
                BadRequest("Failed to create user");
        }

        [HttpPut("update-user")]
        public async Task<ActionResult<string>> UpdateUser(UserRequest request)
        {
            var result = await implementation.UpdateUser(request);
            return !string.IsNullOrEmpty(result) ? 
                Ok(result) : 
                BadRequest("Failed to update user");
        }

        [HttpPut("delete-user/{id}")]
        public async Task<ActionResult<string>> DeleteUser(Guid id)
        {
            var result = await implementation.DeleteUser(id);
            
            return result ? 
                Ok("Successfully deleted user") : 
                BadRequest("Failed to delete user");
        }

        [HttpPut("reset-password")] //Consider privilegded access for this:
        public async Task<ActionResult<bool>> ResetUserPassword([FromQuery] Guid id, [FromBody] string password)
        {
            var result = await implementation.ResetPassword(id, password);
            return result ? Ok(result) : BadRequest(result);
        }

        [HttpPost("import-user")] //Use to import user details for new role or updates
        public async Task<IActionResult> ImportUser(Guid id, ImportRequest request)
        {
            var result = await implementation.ImportUser(id, request);
            return Ok(result);
        }

        [HttpGet("basic-user-details/{id}")]
        public async Task<ActionResult<UserBasicSummary?>> GetBasicUserDetails(Guid id)
        {
            var result = await implementation.GetBasicUserDetails(id);
            return result != null ? Ok(result) : NotFound("User not found");
        }
    }
}
