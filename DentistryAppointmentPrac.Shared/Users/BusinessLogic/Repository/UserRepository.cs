using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository;
using DentistryAppointmentPrac.Shared.Users.DTO;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.BusinessLogic.Repository
{
    public class UserRepository<T>(T dbContext, UserContext userContext) : BaseWritableRepository<User, UserSummary>(dbContext, userContext), IUserRepository where T : DbContext
    {
        public Task<List<UserSummary>> GetAllUserItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PermissionEnum>> GetPermissionsAsync(Guid userId)
        {
            return await dbContext.Set<User>().Where(x => x.Id == userId).SelectMany(p => p.Permissions).ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await dbContext.Set<User>().Where(x => x.Username.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await dbContext.Set<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<User>> GetUsersByPermissionsAsync(PermissionEnum permission)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ResetPassword(Guid id, string password)
        {
            var user = await _dbContext.Set<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null) return false;
            user.Password = PasswordHashHelper.HashPassword(password);
            user.PasswordExpiryDate = DateTime.UtcNow.AddMonths(2);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
