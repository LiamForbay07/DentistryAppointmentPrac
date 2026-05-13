using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Patients.DTO;
using DentistryAppointmentPrac.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Patients
{
    public class PatientRepository<T>(T dbContext, UserContext userContext) 
        : BaseWritableRepository<Patient, PatientSummaryResponse>(dbContext, userContext),IPatientRepository where T : DbContext
    {
    }
}
