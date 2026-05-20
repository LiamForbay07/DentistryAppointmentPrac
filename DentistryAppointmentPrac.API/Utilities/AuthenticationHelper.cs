using DentistryAppointmentPrac.API.Utilities.DatabaseContext;
using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DentistryAppointmentPrac.API.Utilities
{
    public class AuthenticationHelper(DentistAppointmentContext dbContext, IUserRepository repository) : IAuthenticationHelper
    {
        public async Task<User?> GetUserAsync(string email)
        {
            if(Guid.TryParse(email, out Guid userId))
            {
                return await dbContext.User.IgnoreQueryFilters().Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == userId);
            }
            else
            {
                return await dbContext.User.IgnoreQueryFilters().Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Username == email);
            }
        }

        public async Task<List<PermissionEnum>> GetUserPermissionsAsync(Guid id)
        {
            return await repository.GetPermissionsAsync(id);
        }
    }
}
