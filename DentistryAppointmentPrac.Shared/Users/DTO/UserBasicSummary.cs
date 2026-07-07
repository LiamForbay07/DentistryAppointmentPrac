using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.DTO
{
    public class UserBasicSummary : ISummaryHandler<User, UserBasicSummary>, IResponseHandler<User, UserBasicSummary>
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<PermissionEnum> Permissions { get; set; } = new List<PermissionEnum>();

        public static Expression<Func<User, UserBasicSummary>> SummaryMap => model => new UserBasicSummary
        {
            Id = model.Id,
            Username = model.Username,
            DisplayName = model.DisplayName,
            Permissions = model.Permissions
        };

        public static UserBasicSummary? FromEntity(User model)
        {
            return model != null ? new UserBasicSummary
            {
                Id = model.Id,
                Username = model.Username,
                DisplayName = model.DisplayName,
                Permissions = model.Permissions
            } : null;
        }
    }
}
