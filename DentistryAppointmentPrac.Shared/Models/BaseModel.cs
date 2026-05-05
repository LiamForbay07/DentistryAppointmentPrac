using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? UpdatedById { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastUpdated { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
