namespace DentistryAppointmentPrac.Shared.Models
{
    public class Facility : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Provice { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string? GPSLocation { get; set; }
        public bool IsActive { get; set; }
    }
}
