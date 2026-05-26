using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.DentalAppointments.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.RoomDetails.Repository;

namespace DentistryAppointmentPrac.Shared.DentalAppointments.BusinessLogic
{
    public class DentalAppointmentService(IDentalAppointmentRepository repository,IRoomRepository roomRepo, UserContext userContext) : BaseWriteableImplementation<DentalAppointment, DentalAppointmentSummary, DentalAppointmentSummary, DentalAppointmentRequest>(repository), IDentalAppointmentService
    {
        public async Task<string> CancelAppointment(Guid id)
        {
            string result;
            if (id == Guid.Empty) throw new Exception("Appointment does not exist");
            var appointment = await repository.GetTrackedItemByIdAsync(id);
            if(appointment != null)
            {
                appointment.IsAppointmentConfirmed = false;
                appointment.IsDeleted = true;
                repository.UpdateItemAsync(appointment);

                result = await repository.ApplySaveChangesAsync() > 0 ? 
                    $"Successfully cancelled appointment {appointment.RoomDetails?.Name} {appointment.AppointmentDate} {appointment.AppointmentTimeslot}" : 
                    $"Failed to cancel appointment {appointment.RoomDetails?.Name} {appointment.AppointmentDate} {appointment.AppointmentTimeslot}";
            }
            else
            {
                result = $"Failed to find appointment";
            }
            return result;
        }

        public async Task<string> CreateAppointment(DentalAppointmentRequest request)
        {
            var result = string.Empty;
            if (request == null) throw new ArgumentNullException("Invalid request");

            var dentistAppointment = DentalAppointmentRequest.ToEntity(request);
            dentistAppointment.Id = Guid.NewGuid();
            dentistAppointment.UpdatedBy = userContext.Username;
            dentistAppointment.UpdatedById = Guid.Parse(userContext.UserId);

            //Need to add date and time as part of the 
            var isTimeslotBooked = await repository.FindByFilterAsync(x => x.RoomDetailsId == request.RoomDetailsId &&
                                                                           x.AppointmentTimeslot == request.AppointmentTimeslot && 
                                                                           x.AppointmentDate == request.AppointmentDate &&
                                                                           x.IsDeleted == false, 
                                                                           null); 
            
            if(isTimeslotBooked == null)
            {
                repository.AddItemAsync(dentistAppointment);
                result = await repository.ApplySaveChangesAsync() > 0 ? dentistAppointment.Id.ToString() : string.Empty;
            }
            else
            {
                throw new Exception("Timeslot already booked");
            }
            return result;
        }

        public async Task<List<DentalAppointment>> GetAppointmentByDentistId(Guid dentistId)
        {
            if (dentistId == Guid.Empty) throw new NullReferenceException("Dentist not found");

            var dentistAppointments = await repository.FindByFilterAsync(x => x.DentistId == dentistId, null);
            return [.. dentistAppointments];
        }

        public Task<string> UpdateAppointment(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
