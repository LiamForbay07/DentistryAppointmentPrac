using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Users.DTO
{
    public class UserSummary : ISummaryHandler<User, UserSummary>, IResponseHandler<User, UserSummary>
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? EmployeeNumber { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public static Expression<Func<User, UserSummary>> SummaryMap => model => new UserSummary
        {
            Id = model.Id,
            Username = model.Username,
            DisplayName = model.DisplayName,
            EmployeeNumber = "", //Add Employee Number for dentists?
            UserType = model.UserType,
        };

        public static UserSummary? FromEntity(User model)
        {
            return model != null ? new UserSummary
            {
                Id = model.Id,
                DisplayName = model.DisplayName,
                EmployeeNumber = "",
                Username = model.Username,
                UserType = model.UserType
            } : new();
        }
    }
}
