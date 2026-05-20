using DentistryAppointmentPrac.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DentistryAppointmentPrac.Shared.Utilities
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionValidator(PermissionEnum[] permissions, ActionsEnum action = ActionsEnum.And) : Attribute, IAsyncAuthorizationFilter
    {
        public PermissionEnum[] Permissions { get; } = permissions;
        public ActionsEnum Action { get; } = action;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if(!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userContext = context.HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;

            if(userContext == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if(!Permissions.Any())
            {
                return;
            }

            bool hasPermission = Action switch
            {
                ActionsEnum.And => Permissions.All(p => userContext.Permissions.Contains(p)),
                ActionsEnum.Or => Permissions.Any(p => userContext.Permissions.Contains(p)),
                _ => false
            };

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public enum ActionsEnum
    {
        And,
        Or
    }
}
