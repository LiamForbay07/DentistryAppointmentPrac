using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Payment.DTO;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Payment.Repository
{
    public interface IPaymentRepository : IBaseWriteableRepository<Payments,PaymentSummaryResponse>
    {
    }
}
