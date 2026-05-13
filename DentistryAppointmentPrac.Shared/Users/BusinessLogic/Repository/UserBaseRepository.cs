using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.BusinessLogic.Repository
{
    public class UserBaseRepository<TDbContext>(TDbContext context) : IUserBaseRepository where TDbContext : DbContext
    {
        protected readonly TDbContext _dbContext = context;

        public async Task<List<PermissionEnum>> GetPermissionsAsync(Guid userId)
        {
            return await _dbContext.Set<User>().Where(x => x.Id == userId).SelectMany(e => e.Permissions).ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Username.Equals(email));
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ResetPassword(Guid id, string password)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return false;
            user.Password = PasswordHashHelper.HashPassword(password);
            user.PasswordExpiryDate = DateTime.UtcNow.AddMonths(2);
            
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
