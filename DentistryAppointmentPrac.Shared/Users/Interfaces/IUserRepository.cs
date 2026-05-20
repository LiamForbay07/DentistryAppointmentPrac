using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using DentistryAppointmentPrac.Shared.Users.DTO;

namespace DentistryAppointmentPrac.Shared.Users.Interfaces
{
    public interface IUserRepository : IBaseWriteableRepository<User, UserSummary>, IUserBaseRepository
    {
        Task<List<UserSummary>> GetAllUserItemsAsync();
        Task<List<User>> GetUsersByPermissionsAsync(PermissionEnum permission);
    }
}
