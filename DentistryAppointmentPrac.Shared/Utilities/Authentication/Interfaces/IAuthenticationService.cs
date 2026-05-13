using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.DTO;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Authentication.Interfaces
{
    public interface IAuthenticationService: IBaseImplementation
    {
        Task<string> LoginAsync(LoginRequest request);
        Task<bool> ResetPasswordAsync(Guid id, string password);
        Task<List<PermissionEnum>> GetPermissionsAsync();
        Task<UserAuthSummary?> GetMeAsync();
        Task<string> LoginOnDepartmentAsync(Guid id);
    }
}
