using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class LedgerEntry : BaseModel
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public Guid ReferenceId { get; set; }
    }
}
