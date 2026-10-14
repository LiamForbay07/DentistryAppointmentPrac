using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Visits.DTO
{
    public class VisitRequest : IRequestHandler<Visit, VisitRequest>
    {
        public static Visit ToEntity(VisitRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
