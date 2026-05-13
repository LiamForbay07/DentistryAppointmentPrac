using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Patients.DTO
{
    public class PatientSummaryResponse : ISummaryHandler<Patient, PatientSummaryResponse>, IResponseHandler<Patient, PatientSummaryResponse>
    {
        public static Expression<Func<Patient, PatientSummaryResponse>> SummaryMap => throw new NotImplementedException();

        public static PatientSummaryResponse? FromEntity(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
