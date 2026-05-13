using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.DTO
{
    public class UserAuthSummary
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<PermissionEnum> Permissions = [];
        public static UserAuthSummary? FromEntity(User? model)
        {
            if (model == null) return null;
            return new UserAuthSummary
            {
                Id = model.Id,
                DisplayName = model.DisplayName,
                Username = model.Username,
                Permissions = model.Permissions
            };
        }
    }
}
