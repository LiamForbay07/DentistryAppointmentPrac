using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class UserContext
    {
        public string UserId { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public Guid DentistDepartmentId { get; set; }
        public List<PermissionEnum> Permissions { get; set; } = [];
    }
}
