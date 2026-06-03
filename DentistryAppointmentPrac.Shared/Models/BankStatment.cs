using System.ComponentModel.DataAnnotations;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class BankStatment
    {
        [Key]
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public List<Transactions> Transactions { get; set; } = new List<Transactions>();
    }
}
