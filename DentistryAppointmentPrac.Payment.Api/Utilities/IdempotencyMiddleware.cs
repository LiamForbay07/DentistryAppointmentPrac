using DentistryAppointmentPrac.Payment.Api.Models;
using DentistryAppointmentPrac.Payment.Api.Utilities.Database;
using DentistryAppointmentPrac.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DentistryAppointmentPrac.Payment.Api.Utilities
{
    public class IdempotencyMiddleware
    {
        private readonly RequestDelegate _next;

        public IdempotencyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, PaymentAppointmentContext dbContext)
        {
            var key = context.Request.Headers["Idempotency-Key"].FirstOrDefault();
            if (string.IsNullOrEmpty(key))
            {
                await _next(context);
                return;
            }

            var existing = await dbContext.Set<IdempotencyRecord>()
                            .FirstOrDefaultAsync(x => x.Key == key);

            if (existing != null)
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(existing.Response);
                return;
            }

            var originalBody = context.Response.Body;
            using var ms = new MemoryStream();
            context.Response.Body = ms;

            await _next(context);

            ms.Position = 0;
            var response = await new StreamReader(ms).ReadToEndAsync();

            dbContext.Add(new IdempotencyRecord
            {
                Key = key,
                Response = response
            });
            await dbContext.SaveChangesAsync();

            ms.Position = 0;
            await ms.CopyToAsync(originalBody);
        }
    }
}

