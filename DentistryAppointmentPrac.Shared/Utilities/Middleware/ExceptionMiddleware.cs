using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DentistryAppointmentPrac.Shared.Utilities.Middleware
{
    public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				ProblemDetails? problem;
				if(ex.GetType() == typeof(UnauthorizedAccessException))
				{
					problem = new ProblemDetails
					{
						Status = (int)HttpStatusCode.Unauthorized,
						Detail = ex.Message,
						Title = "Unauthorized",
						Instance = $"{context.Request.Method} {context.Request.Path}"
					}; 
				}else if(ex.GetType() == typeof(DbUpdateException) && ex.InnerException.Message.ToLower().Contains("duplicate key"))
				{
					problem = new ProblemDetails
					{
						Status = (int)HttpStatusCode.BadRequest,
						Detail = "Result could not be saved. Please ensure it is not a duplicate",
						Title = "Something went wrong",
						Instance = $"{context.Request.Method} {context.Request.Path}"
					};
				}
				else
				{
					problem = new ProblemDetails
					{
						Status = (int)HttpStatusCode.BadRequest,
						Detail = ex.Message,
						Title = "Something went wrong",
						Instance = $"{context.Request.Method} {context.Request.Path}"
					};
				}
				context.Response.StatusCode = problem.Status.Value;
				context.Response.ContentType = "application/problem+json";

				var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var json = JsonSerializer.Serialize(problem, options);

				await context.Response.WriteAsync(json);
			}
        }
    }
}
