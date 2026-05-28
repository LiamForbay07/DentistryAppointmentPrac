using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OfflineFiles.DTO
{
    public class OfflineFileSummaryResponse : ISummaryHandler<OfflineFile, OfflineFileSummaryResponse>, IResponseHandler<OfflineFile, OfflineFileSummaryResponse>
    {
        public static Expression<Func<OfflineFile, OfflineFileSummaryResponse>> SummaryMap => throw new NotImplementedException();

        public static OfflineFileSummaryResponse? FromEntity(OfflineFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
