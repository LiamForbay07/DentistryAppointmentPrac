using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Payment.DTO;
using DentistryAppointmentPrac.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Payment.Repository
{
    public class PaymentRepository<TContext>(TContext dbContext, UserContext userContext) : BaseWritableRepository<Payments, PaymentSummaryResponse>(dbContext, userContext), IPaymentRepository
        where TContext : DbContext
    {
    }
}
