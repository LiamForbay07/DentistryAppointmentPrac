using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.Patients.DTO
{
    public class PatientRequest : IRequestHandler<Patient, PatientRequest>
    {
        public static Patient ToEntity(PatientRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
