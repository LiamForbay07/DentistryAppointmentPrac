using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Patients.DTO
{
    public class PatientRequest : IRequestHandler<Patient, PatientRequest>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public string? MedicalAidNo { get; set; }


        public static Patient ToEntity(PatientRequest request)
        {
            if (request == null) return new();
            return new()
            {
                Name = request.Name,
                MedicalAidNo = request.MedicalAidNo,
                IdNumber = request.IdNumber,
                Surname = request.Surname,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                LastUpdated = DateTime.UtcNow
            };
        }
    }
}
