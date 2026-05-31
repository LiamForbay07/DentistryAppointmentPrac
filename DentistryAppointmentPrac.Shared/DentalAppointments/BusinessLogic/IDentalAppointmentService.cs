using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.DentalAppointments.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DentalAppointments.BusinessLogic
{
    public interface IDentalAppointmentService  : IImplementation<DentalAppointment, DentalAppointmentSummary, DentalAppointmentSummary>
    {
        Task<string> CreateAppointment(DentalAppointmentRequest request);
        Task<List<DentalAppointment>> GetAppointmentByDentistId(Guid dentistId);
        Task<string> UpdateAppointment(Guid id, UpdateAppointmentDetailsRequest request);
        Task<string> CancelAppointment(Guid id);
    }
}
