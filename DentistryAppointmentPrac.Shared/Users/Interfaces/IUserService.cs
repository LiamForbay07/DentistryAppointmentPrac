using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.DTO;
using DentistryAppointmentPrac.Shared.Utilities.Imports.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.Interfaces
{
    public interface IUserService : IImplementation<User, UserSummary, UserSummary>
    {
        Task<string> CreateUser(UserRequest request);
        Task<string> UpdateUser(UserRequest request);
        Task<bool> DeleteUser(Guid id);
        Task<bool> ResetPassword(Guid id, string password);
        Task<string> ImportUser(Guid id, ImportRequest request);
    }
}
