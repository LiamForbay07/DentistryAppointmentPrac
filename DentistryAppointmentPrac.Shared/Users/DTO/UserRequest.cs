using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.DTO
{
    public class UserRequest : IRequestHandler<User, UserRequest>
    {
        public Guid? Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<PermissionEnum> Permissions { get; set; } = [];
        public UserType UserType { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime? PasswordExpiryDate { get; set; }

        public static User ToEntity(UserRequest request)
        {
            if (request == null) return new();
            return new User
            {
                Id = (Guid)((request.Id != Guid.Empty || request.Id != null) ? request.Id : Guid.NewGuid())!,
                Title = request.Title,
                Username = request.Username,
                Permissions = request.Permissions,
                UserType = request.UserType,
                Name = request.Name,
                Surname = request.Surname,
                DisplayName = request.DisplayName,
                Password = PasswordHashHelper.HashPassword(request.Password),
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
