using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.DTO;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities.Imports.DTO;

namespace DentistryAppointmentPrac.Shared.Users.BusinessLogic
{
    public class UserService(IUserRepository repository, UserContext userContext) : BaseWriteableImplementation<User, UserSummary, UserSummary, UserRequest>(repository), IUserService
    {
        public async Task<string> CreateUser(UserRequest request)
        {
            var result = string.Empty;
            if (request == null)
                return result;

            User user = UserRequest.ToEntity(request);
            repository.AddItemAsync(user);

            result = await repository.ApplySaveChangesAsync() > 0 ? user.Id.ToString() : "Failed to create user";
            return result;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            if (id == Guid.Empty) return false;
            var user = await repository.GetItemByIdAsync(id);

            bool result;
            result = user != null && await repository.DeleteItemAsync(id);

            return result;
        }

        public async Task<UserBasicSummary?> GetBasicUserDetails(Guid id)
        {
            if(id == Guid.Empty) ArgumentNullException.ThrowIfNull("Invalid user");

            var user = await repository.GetItemByIdAsync(id);
            
            if(user == null) return null;
            return UserBasicSummary.FromEntity(user);
        }

        public Task<string> ImportUser(Guid id, ImportRequest request)
        {
            throw new NotImplementedException(); //Quick Import and save to file stream
        }

        public async Task<bool> ResetPassword(Guid id, string password)
        {
           return await repository.ResetPassword(id, password);
        }

        public async Task<string> UpdateUser(UserRequest request)
        {
            if (request.Id == Guid.Empty || request == null)
                throw new ArgumentNullException($"{nameof(request)}"); //Come back to this

            var user = UserRequest.ToEntity(request);
            repository.UpdateItemAsync(user);

            return await repository.ApplySaveChangesAsync() > 0 ? "User updated successfully" : "User update failed";
        }
    }
}
