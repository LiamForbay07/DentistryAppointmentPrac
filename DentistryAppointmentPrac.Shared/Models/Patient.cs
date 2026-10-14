namespace DentistryAppointmentPrac.Shared.Models
{
    public class Patient : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Cellphone { get; set; } 
        public string? Email { get; set; } 
        public string? MedicalAidNo { get; set; }
    }
}
