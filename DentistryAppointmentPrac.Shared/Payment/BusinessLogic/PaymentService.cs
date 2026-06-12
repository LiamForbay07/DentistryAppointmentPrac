using DentistryAppointmentPrac.Shared.Payment.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Payment.BusinessLogic
{
    public class PaymentService(IPaymentRepository repository) : IPaymentService
    {
        public Task<Guid> CreatePayment(Guid userId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProcessPayment(Guid paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
