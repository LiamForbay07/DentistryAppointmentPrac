using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using DentistryAppointmentPrac.Shared.RoomDetails.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.Repository
{
    public class RoomRepository<T>(T dbContext, UserContext userContext) : BaseWritableRepository<RoomDetail, RoomDetailResponseSummary>(dbContext, userContext), IRoomRepository
        where T: DbContext
    {
    }
}
