using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.Interfaces
{
    public interface IUserBaseRepository
    {
        Task<List<PermissionEnum>> GetPermissionsAsync(Guid userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> ResetPassword(Guid id, string password);
        Task<User?> GetUserByIdAsync(Guid id);
        
    }
}
