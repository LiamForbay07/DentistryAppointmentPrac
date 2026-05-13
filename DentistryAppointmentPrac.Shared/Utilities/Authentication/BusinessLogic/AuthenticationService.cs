using DentistryAppointmentPrac.Shared.DentalAppointments;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.DTO;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.DTO;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Authentication.BusinessLogic
{
    public class AuthenticationService(IConfiguration configuration, IUserBaseRepository repository, UserContext userContext) : IAuthenticationService
    {
        public async Task<UserAuthSummary?> GetMeAsync()
        {
            var user = await repository.GetUserByEmailAsync(userContext.Username);
            return UserAuthSummary.FromEntity(user);
        }

        public async Task<List<PermissionEnum>> GetPermissionsAsync()
        {
            if(!Guid.TryParse(userContext.UserId, out Guid userId)) return [];

            var user = await repository.GetUserByIdAsync(userId);
            return user?.Permissions.ToList() ?? [];
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await repository.GetUserByEmailAsync(request.Username) ?? 
                       throw new UnauthorizedAccessException("Invalid Credentials");

            var valid = PasswordHashHelper.VerifyPassword(request.Password, user.Password);
            if (!valid) throw new UnauthorizedAccessException("Invalid Credentials");

            if (user.PasswordExpiryDate != null && user.PasswordExpiryDate < DateTime.Now) throw new UnauthorizedAccessException("Password expired");

            var token = DentistryAppointmentJwtSecurityTokenHandler.GenerateToken(configuration, user.Username, null);
            return token;
        }

        public Task<string> LoginOnDepartmentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ResetPasswordAsync(Guid id, string password)
        {
            return (userContext.Permissions.Contains(PermissionEnum.Admin) ||
                    userContext.UserId.Equals(id.ToString())) &&
                    await repository.ResetPassword(id, password);   
        }
    }
}
