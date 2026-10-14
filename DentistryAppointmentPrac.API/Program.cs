
using DentistryAppointmentPrac.API.Utilities;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Utilities;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.Controllers;
using DentistryAppointmentPrac.Shared.Utilities.Middleware;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

namespace DentistryAppointmentPrac.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<UserContext>();
            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters
                .Add(new JsonStringEnumConverter());
            })
            .AddApplicationPart(typeof(AuthenticationController).Assembly)
            .AddControllersAsServices();
            
            InitializeHelper.InitializeAppsettingsAndEnvironmentVariables(builder);
            InitializeHelper.InitializeAuthentication(builder);
            InitializeHelper.InitializeServices(builder);
            InitializeHelper.InitializeRepositories(builder);
            InitializeHelper.InitializeBusinessLogic(builder);
            InitializeHelper.InitializeAiServices(builder);
            InitializeHelper.InitializeValidators(builder);
            //InitializeHelper.InitilizeStorageServices(builder);
            
            builder.Services.AddSignalR();
            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("MultiToken", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes("Manual");
                    policy.AddRequirements(new UserContextRequirement());
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                       .WithTitle("My API")
                       .WithTheme(ScalarTheme.Moon)
                       .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });

            app.UseHttpsRedirection();

            InitializeDocumentControllers.MapDocumentControllers(app);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
