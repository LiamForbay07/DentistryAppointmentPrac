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

        public async Task<string> UpdateAppointment(Guid id, UpdateAppointmentDetailsRequest request)
        {
            var appointment = await repository.GetItemByIdAsync(id) ?? throw new NullReferenceException("Appointment not available");
            appointment.AppointmentDate = request.AppointmentDate;
            appointment.AppointmentTimeslot = request.AppointmentTimeslot;
            appointment.DentistId = request.DentistId;
            appointment.UserId = request.UserId;
            appointment.LastUpdated = DateTime.UtcNow;
            appointment.UpdatedBy = userContext.Username;
            appointment.UpdatedById = Guid.Parse(userContext.UserId);

            repository.UpdateItemAsync(appointment);
            bool result = await repository.ApplySaveChangesAsync() > 0;
            return result ? $"Updated appointment successfully to {request.AppointmentDate} : {request.AppointmentTimeslot} : Room {appointment.RoomDetails?.Name}":
                            "Failed to update appointment";
        }
    }
}
