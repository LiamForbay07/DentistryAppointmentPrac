using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Payment.DTO
{
    public class PaymentRequest : IRequestHandler<Payments, PaymentRequest>
    {
        public static Payments ToEntity(PaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
