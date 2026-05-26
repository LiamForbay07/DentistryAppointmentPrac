using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.RoomDetails.DTO
{
    public class RoomDetailResponseSummary : ISummaryHandler<RoomDetail, RoomDetailResponseSummary>, IResponseHandler<RoomDetail, RoomDetailResponseSummary>
    {
        public static Expression<Func<RoomDetail, RoomDetailResponseSummary>> SummaryMap => throw new NotImplementedException();

        public static RoomDetailResponseSummary? FromEntity(RoomDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
