
using DentistryAppointmentPrac.API.Utilities;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Utilities;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.Controllers;
using DentistryAppointmentPrac.Shared.Utilities.Middleware;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

namespace DentistryAppointmentPrac.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<UserContext>();
            builder.Services.AddSignalR();
            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters
                .Add(new JsonStringEnumConverter());
            })
            .AddApplicationPart(typeof(AuthenticationController).Assembly)
            .AddControllersAsServices();

            InitializeHelper.InitializeAuthentication(builder);

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("MultikToken", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes("AzureB2C", "Manual");
                    policy.AddRequirements(new UserContextRequirement());
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                       .WithTitle("My API")
                       .WithTheme(ScalarTheme.Moon)
                       .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
