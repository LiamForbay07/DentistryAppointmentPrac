using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Patients.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Patients.BusinessLogic
{
    public interface IPatientService : IWriteableImplementation<Patient,PatientSummaryResponse, PatientSummaryResponse, PatientRequest>
    {
        Task<string> CreatePatient(PatientRequest request);
        Task<string> UpdatePatient(Guid id,PatientRequest request);
        Task<string> ReschedulePatient(PatientRequest request);
        Task<string> SendEmail(string toEmailAddress, string subject, string body);
        Task<bool> DeletePatient(Guid id);
    }
}
