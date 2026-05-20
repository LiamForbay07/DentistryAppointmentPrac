using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Interfaces
{
    public interface IAuthenticationHelper
    {
        Task<User?> GetUserAsync(string email);
        Task<List<PermissionEnum>> GetUserPermissionsAsync(Guid id);
    }
}
