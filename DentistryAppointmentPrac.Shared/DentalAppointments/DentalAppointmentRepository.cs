using DentistryAppointmentPrac.Shared.DentalAppointments.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DentalAppointments
{
    public class DentalAppointmentRepository<T>(T dbContext, UserContext userContext) : BaseWritableRepository<DentalAppointment, DentalAppointmentSummary>(dbContext, userContext), IDentalAppointmentRepository where T : DbContext
    {
    }
}
