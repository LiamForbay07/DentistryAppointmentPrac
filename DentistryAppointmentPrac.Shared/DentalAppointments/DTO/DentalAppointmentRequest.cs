using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DentalAppointments.DTO
{
    public class DentalAppointmentRequest : IRequestHandler<DentalAppointment, DentalAppointmentRequest>
    {
        public static DentalAppointment ToEntity(DentalAppointmentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
