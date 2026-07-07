using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.DTO;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.DTO;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Authentication.Controllers
{
    [Route("auth")]
    public class AuthenticationController : BaseController<IAuthenticationService>
    {
        private readonly IUserService _userService;
        public AuthenticationController(IAuthenticationService implementation, IUserService userService) : base(implementation) { 
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<OkObjectResult> Login(LoginRequest request)
        {
            var token = await implementation.LoginAsync(request);
            Response.Cookies.Append("auth_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
            return new OkObjectResult(new { Token = token, Success = true });
        }

        [HttpPost("register-user")]
        public async Task<ActionResult> RegisterUser(UserRequest request)
        {
            var result = await _userService.CreateUser(request);
            return !string.IsNullOrEmpty(result) ? Ok(result) : BadRequest("Failed to create user");
        }

        [HttpPost("{id}/reset-password")]
        [Authorize(Policy = "MultiToken")]
        public async Task<OkObjectResult> ResetUserPassword(Guid id, string password)
        {
            var result = await implementation.ResetPasswordAsync(id, password);
            return new OkObjectResult(new { Success = result, Message = result ? "Your password has been changed." : "Failed to reset password." });
        }

        [HttpGet("permissions")]
        [Authorize(Policy = "MultiToken")]
        public async Task<OkObjectResult> PermissionsAsync()
        {
            var result = await implementation.GetPermissionsAsync();
            return new OkObjectResult(result);
        }

        [HttpGet("me")]
        [Authorize(Policy = "MultiToken")]
        public async Task<OkObjectResult> GetMeAsync()
        {
            var result = await implementation.GetMeAsync();
            return new OkObjectResult(result);
        }

        [HttpPost("login-on-behalf")]
        [Authorize(Policy ="MultiToken")]
        [PermissionValidator([PermissionEnum.TechSupport])]
        public async Task<OkObjectResult> LoginOnBehalfAsync(LoginRequest loginRequest)
        {
            return new OkObjectResult("");
        }
    }
}
