using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class Payments : BaseModel
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
    }
}
