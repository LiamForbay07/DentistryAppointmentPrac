namespace DentistryAppointmentPrac.Shared.Models
{
    public class Visit : BaseModel
    {
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; } = new();
        public Guid? AppointmentId { get; set; }
        public virtual Appointment? Appointment { get; set; }
        public Guid FacilityId { get; set; }
        public virtual Facility Facility { get; set; } = new();
        public VisitType VisitType { get; set; }
        public DateTime ArrivalTime { get; set; }
        public AppointmentStages Status { get; set; }
        public string QueueNumber { get; set; } = string.Empty; //QUEUE NUMBER FOR WALK-IN OR BOOKEDVISITS
    }

    public enum VisitType
    {
        WalkIn,
        Appointment, 
        Emergency
    }
}
