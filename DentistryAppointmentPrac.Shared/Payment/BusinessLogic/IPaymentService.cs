using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Payment.BusinessLogic
{
    public interface IPaymentService 
    {
        Task<Guid> CreatePayment(Guid userId, decimal amount);
        Task<bool> ProcessPayment(Guid paymentId);
    }
}
