using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.DentalAppointments;
using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Facilities.Repository;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Patients;
using DentistryAppointmentPrac.Shared.Patients.DTO;
using DentistryAppointmentPrac.Shared.Visits.DTO;
using DentistryAppointmentPrac.Shared.Visits.Interfaces;

namespace DentistryAppointmentPrac.Shared.Visits.BusinessLogic
{
    public class VisitService(IVisitRepository repository, IPatientRepository patientRepo,IDentalAppointmentRepository dentalAppointmentRepo, IFacilityRepository facilityRepo, UserContext userContext) : BaseWriteableImplementation<Visit, VisitResponse, VisitResponse, VisitRequest>(repository) ,IVisitService
    {
        public async Task<bool> CancelVisit(Guid id)
        {
            var existingVisit = await repository.GetTrackedItemByIdAsync(id);
            if (existingVisit == null)
                return false;

            existingVisit.Status = AppointmentStages.Cancelled;
            existingVisit.UpdatedBy = userContext.Username;
            existingVisit.UpdatedById = Guid.Parse(userContext.UserId);

            repository.UpdateItemAsync(existingVisit);
            return await repository.ApplySaveChangesAsync() > 0;
        }

        public async Task<string> CreateVisit(CreateVisitRequest request)
        {
            var doesPatientExist =  await patientRepo.GetItemByIdAsync(request.PatientId) ?? null;
            var doesFacilityExist = await facilityRepo.GetItemByIdAsync(request.FacilitiyId) ?? null;

            if (doesFacilityExist == null && doesPatientExist == null)
                return string.Empty;

            var result = new Visit
            {
                Id = Guid.NewGuid(),
                Status = request.Status,
                ArrivalTime = request.ArrivalTime,
                QueueNumber = $"WN-{Guid.NewGuid().ToString("N")[..4]}",
                VisitType = request.VisitType,
                FacilityId = request.FacilitiyId,
                PatientId = request.PatientId,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = userContext.Username,
                UpdatedById = Guid.Parse(userContext.UserId)
            };

            repository.AddItemAsync(result);
            return await repository.ApplySaveChangesAsync() > 0 ? result.Id.ToString() : string.Empty;
        }

        public async Task<string> CreateWalkInVisit(CreateWalkInVisitRequest request)
        {
            var result = new Visit();
            var existingFacility = await facilityRepo.GetItemByIdAsync(request.FacilitiyId) ?? null;
            var existingPatient = request.PatientDetails != null ? 
                await patientRepo.FindSingleItemByFilterAsync(x => x.IdNumber == request.PatientDetails.IdNumber) : 
                null;

            if (existingFacility == null)
            {
                return string.Empty;
            }

            if (existingPatient == null)
            {
                var patientDetails = await CreatePatientForWalkIn(request.PatientDetails);
                if (!string.IsNullOrEmpty(patientDetails))
                {
                    result = CreateWalkInVisitRequest.ToEntity(request);
                    result.PatientId = Guid.Parse(patientDetails);
                    result.FacilityId = existingFacility.Id;
                    result.UpdatedBy = userContext.Username;
                    result.UpdatedById = Guid.Parse(userContext.UserId);
                }
                else
                    result.PatientId = Guid.Empty;
            }
            else
            {
                result = CreateWalkInVisitRequest.ToEntity(request);
                result.FacilityId = existingFacility.Id;
                result.PatientId = existingPatient.Id;
                result.UpdatedBy = userContext.Username;
                result.UpdatedById = Guid.Parse(userContext.UserId);
                repository.AddItemAsync(result);
            }
            return await repository.ApplySaveChangesAsync() > 0 && result.Id != Guid.Empty ? result.Id.ToString() : string.Empty;
        }

        private async Task<string> CreatePatientForWalkIn(PatientRequest? request)
        {
            if (request == null)
                return string.Empty;

            var patient = PatientRequest.ToEntity(request);
            patientRepo.AddItemAsync(patient);
            
            return await patientRepo.ApplySaveChangesAsync() > 0 ? patient.Id.ToString() : string.Empty;
        }

        public async Task<bool> DeleteVisit(Guid id)
        {
            return await repository.DeleteItemAsync(id);
        }

        public async Task<PageData<VisitSummary?>> SearchVisitByFacilityId(Guid facilityId, VisitSummaryPagedRequest request)
        {
            return await repository.GetPagedVisitByFacilityId(facilityId, request);
        }

        public async Task<PageData<VisitSummary?>> SearchVisitsForAllFacilities(VisitSummaryPagedRequest request)
        {
            return await repository.GetPagedVisitsForAllFacilities(request);
        }

        public async Task<bool> UpdateVisitArrivalTime(UpdateVisitRequest request)
        {
            if (request.VisitId == Guid.Empty)
                return false;
            
            var visit = await repository.GetItemByIdAsync(request.VisitId);
            if(visit == null) return false;
            
            visit.ArrivalTime = request.ArrivalTime;
            repository.UpdateItemAsync(visit);

            return await repository.ApplySaveChangesAsync() > 0;
        }
    }
}
