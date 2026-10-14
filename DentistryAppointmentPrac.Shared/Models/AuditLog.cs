using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class AuditLog
    {
        [Key]
        public Guid AuditId { get; set; }
        public Guid UserId { get; set; }
        public Guid FacilityId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; } = DateTime.Now;
    }
}
