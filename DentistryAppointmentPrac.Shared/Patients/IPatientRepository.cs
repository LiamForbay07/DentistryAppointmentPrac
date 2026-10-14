using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Patients.DTO;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Patients
{
    public interface IPatientRepository: IBaseWriteableRepository<Patient, PatientSummaryResponse>
    {
        Task<PageData<PatientSummaryResponse?>> GetPatientsBySearch(GetPatientSummaryPagedRequest request);
    }
}
