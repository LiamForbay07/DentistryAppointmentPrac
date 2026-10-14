using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.DTO;
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
            string result;

            var patient = PatientRequest.ToEntity(request);
            patient.UpdatedBy = userContext.Username;
            patient.UpdatedById = Guid.Parse(userContext.UserId);

            repository.AddItemAsync(patient);
            result = await repository.ApplySaveChangesAsync() > 0 ? patient.Id.ToString() : $"Failed to create patient {request.Name} {request.Surname}";
            return result;
       }

        public async Task<bool> DeletePatient(Guid id)
        {
            return await repository.DeleteItemAsync(id);
        }

        public async Task<bool> UpdatePatient(UpdatePatientRequest request)
        {
            bool result;
            var patient = await repository.GetItemByIdAsync(request.PatientId);
            if (patient == null) result = false;
            else
            {
                var updatePatient = UpdatePatientRequest.ToEntity(request);
                updatePatient.UpdatedBy = userContext.Username;
                updatePatient.UpdatedById = Guid.Parse(userContext.UserId);

                repository.UpdateItemAsync(updatePatient);
                result = await repository.ApplySaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<PageData<PatientSummaryResponse?>> GetPagedPatientsAsync(GetPatientSummaryPagedRequest request)
        {
            var result = await repository.GetPatientsBySearch(request);
            return result;
        }
    }
}
