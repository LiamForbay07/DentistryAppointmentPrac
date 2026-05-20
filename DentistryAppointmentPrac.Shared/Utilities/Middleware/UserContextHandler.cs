using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Utilities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DentistryAppointmentPrac.Shared.Utilities.Middleware
{
    public class UserContextHandler : AuthorizationHandler<UserContextRequirement>
    {
        private readonly IAuthenticationHelper _helper;
        private readonly UserContext _userContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextHandler(IAuthenticationHelper helper, UserContext userContext, IHttpContextAccessor httpContextAccessor)
        {
            _helper = helper;
            _userContext = userContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            UserContextRequirement requirement)
        {
            var principal = context.User;

            if (!principal.Identity?.IsAuthenticated ?? true)
                return;

            var subjectId = principal.FindFirst(ClaimTypes.Name)?.Value ?? "";

            if (string.IsNullOrEmpty(subjectId))
                return;

            var user = await _helper.GetUserAsync(subjectId);
            if(user == null)
            {
                context.Fail();
                return;
            }

            var departmentId = "";
            var isTechSupport = user.Permissions.Contains(PermissionEnum.TechSupport);
            if (isTechSupport)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if(httpContext != null)
                {
                    departmentId = httpContext.Request.Headers["Department"].ToString();
                }
            }
            else
            {
                departmentId = _userContext.DentistDepartmentId.ToString() ?? "";
            }

            _userContext.UserId = user?.Id.ToString() ?? "";
            _userContext.DentistDepartmentId = Guid.TryParse(departmentId, out Guid depId) ? depId : Guid.NewGuid();
            _userContext.User = user?.DisplayName ?? "";
            _userContext.Username = user?.Username ?? "";
            _userContext.Permissions = user?.Permissions?.ToList() ?? [];

            var superUserIdClaim = principal.FindFirst(DentistryAppointmentJwtSecurityTokenHandler.TechSupportIdClaim)?.Value;
            var superUserNameClaim = principal.FindFirst(DentistryAppointmentJwtSecurityTokenHandler.TechSupportUserNameClaim)?.Value;
            if (!string.IsNullOrEmpty(superUserIdClaim))
            {
                _userContext.TechSupportId = superUserIdClaim;
                _userContext.TechSupportUserName = superUserNameClaim;
            }
            context.Succeed(requirement);
        }
    }
}
