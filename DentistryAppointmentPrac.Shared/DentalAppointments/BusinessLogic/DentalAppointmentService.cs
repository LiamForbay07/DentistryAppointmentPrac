using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.DentalAppointments.DTO;
using DentistryAppointmentPrac.Shared.Models;

namespace DentistryAppointmentPrac.Shared.DentalAppointments.BusinessLogic
{
    public class DentalAppointmentService(IDentalAppointmentRepository repository, UserContext userContext) : BaseWriteableImplementation<DentalAppointment, DentalAppointmentSummary, DentalAppointmentSummary, DentalAppointmentRequest>(repository), IDentalAppointmentService
    {
        public Task<string> CancelAppointment(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateAppointment(DentalAppointmentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<DentalAppointment>> GetAppointmentByDentistId(Guid dentistId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAppointment(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
