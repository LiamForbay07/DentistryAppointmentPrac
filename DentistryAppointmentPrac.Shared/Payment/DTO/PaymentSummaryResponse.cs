using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Payment.DTO
{
    public class PaymentSummaryResponse : ISummaryHandler<Payments, PaymentSummaryResponse>, IResponseHandler<Payments, PaymentSummaryResponse>
    {
        public static Expression<Func<Payments, PaymentSummaryResponse>> SummaryMap => throw new NotImplementedException();

        public static PaymentSummaryResponse? FromEntity(Payments entity)
        {
            throw new NotImplementedException();
        }
    }
}
