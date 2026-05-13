using DentistryAppointmentPrac.Shared.DentalAppointments.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DentalAppointments
{
    public interface IDentalAppointmentRepository : IBaseWriteableRepository<DentalAppointment, DentalAppointmentSummary>
    {
    }
}
