using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DentalAppointments.DTO
{
    public class DentalAppointmentSummary : IResponseHandler<DentalAppointment, DentalAppointmentSummary>, ISummaryHandler<DentalAppointment, DentalAppointmentSummary>
    {
        //Add Summary Details here
        public static Expression<Func<DentalAppointment, DentalAppointmentSummary>> SummaryMap => e => new DentalAppointmentSummary
        {
            
        };

        public static DentalAppointmentSummary? FromEntity(DentalAppointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
