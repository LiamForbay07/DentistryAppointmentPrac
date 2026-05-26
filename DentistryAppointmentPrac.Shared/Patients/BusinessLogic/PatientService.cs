using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Patients.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Patients.BusinessLogic
{
    public class PatientService(IPatientRepository repository, UserContext userContext)
        : BaseWriteableImplementation<Patient, PatientSummaryResponse, PatientSummaryResponse, PatientRequest>(repository),
        IPatientService
    {
        public async Task<string> CreatePatient(PatientRequest request)
        {
            var result = string.Empty;
            if (request == null)
            {
                throw new ArgumentNullException("Patient request is null");
            }

            var patientModel = PatientRequest.ToEntity(request);
            patientModel.Id = Guid.NewGuid();
            patientModel.UpdatedBy = userContext.Username;
            patientModel.UpdatedById = Guid.Parse(userContext.UserId);
            
            repository.AddItemAsync(patientModel);

            result = await repository.ApplySaveChangesAsync() > 0 ? patientModel.Id.ToString() :  string.Empty;
            return result;
        }

        public async Task<bool> DeletePatient(Guid id)
        {
           bool result = false;
           if(id == Guid.Empty) throw new ArgumentNullException("Patient not found");

            var patient = await repository.GetItemByIdAsync(id);
            if (patient == null)
                result = false;
            else
            {
                patient.IsDeleted = true;
                result = true;
            }
            return result;
        }

        public Task<string> ReschedulePatient(PatientRequest request) // doesnt belong here
        {
            throw new NotImplementedException();
        }

        public Task<string> SendEmail(string toEmailAddress, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdatePatient(Guid id, PatientRequest request)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentNullException("Patient not found");
            }

            var patient = await repository.GetItemByIdAsync(id);
            if (patient == null)
                return string.Empty;

            patient.Name = request.Name;
            patient.Surname = request.Surname;
            patient.IdNumber = request.IdNumber;
            patient.UpdatedBy = userContext.Username;
            patient.UpdatedById = Guid.Parse(userContext.UserId);


            repository.UpdateItemAsync(patient);

            return await repository.ApplySaveChangesAsync() > 0 ? "Updated successfully" : "Failed to update patient";
        }
    }
}
