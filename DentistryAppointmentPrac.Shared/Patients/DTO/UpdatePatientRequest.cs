using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Patients.DTO
{
    public class UpdatePatientRequest : IRequestHandler<Patient, UpdatePatientRequest>
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? MedicalAidNo { get; set; }
        public static Patient ToEntity(UpdatePatientRequest request)
        {
            if(request == null) return new();
            return new Patient
            {
                Id = request.PatientId,
                Name = request.Name,
                Surname = request.Surname,
                IdNumber = request.IdNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Cellphone = request.Cellphone,
                Email = request.Email,
                MedicalAidNo = request.MedicalAidNo,
                LastUpdated = DateTime.UtcNow
            };
        }
    }
}
