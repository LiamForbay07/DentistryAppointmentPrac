using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System.Linq.Expressions;

namespace DentistryAppointmentPrac.Shared.Patients.DTO
{
    public class PatientSummaryResponse : ISummaryHandler<Patient, PatientSummaryResponse>, IResponseHandler<Patient, PatientSummaryResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? MedicalAidNo { get; set; }

        public static Expression<Func<Patient, PatientSummaryResponse>> SummaryMap => x => new PatientSummaryResponse
        {
            Id = x.Id,
            Name = x.Name,
            Surname = x.Surname,
            IdNumber = x.IdNumber,
            DateOfBirth = x.DateOfBirth,
            Gender = x.Gender,
            Cellphone = x.Cellphone,
            Email = x.Email,
            MedicalAidNo = x.MedicalAidNo
        };

        public static PatientSummaryResponse? FromEntity(Patient entity)
        {
            if(entity == null) return null;
            return SummaryMap.Compile().Invoke(entity);
        }
    }
}
