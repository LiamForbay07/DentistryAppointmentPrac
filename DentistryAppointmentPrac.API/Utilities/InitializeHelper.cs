using Azure.Storage.Blobs;
using DentistryAppointmentPrac.API.Utilities.DatabaseContext;
using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.DentalAppointments;
using DentistryAppointmentPrac.Shared.DentalAppointments.BusinessLogic;
using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.RoomDetails.Repository;
using DentistryAppointmentPrac.Shared.Users.BusinessLogic.Repository;
using DentistryAppointmentPrac.Shared.Users.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.BusinessLogic;
using DentistryAppointmentPrac.Shared.Utilities.Authentication.Interfaces;
using DentistryAppointmentPrac.Shared.Utilities.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DentistryAppointmentPrac.API.Utilities
{
    internal static class InitializeHelper
    {
        internal static void InitializeAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "MultiScheme";
                options.DefaultChallengeScheme = "MultiScheme";
            })
            .AddPolicyScheme("MultiScheme", "Multi Scheme", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                    if (authHeader != null && authHeader.StartsWith("Bearer "))
                    {
                        return "Manual";
                    }
                    return "Manual";
                };
            })
            .AddJwtBearer("Manual", options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }

        internal static void InitializeRepositories(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["ConnectionStrings:ApprointPracDB"];
            if (string.IsNullOrEmpty(connectionString)) {
                throw new InvalidOperationException("Please provide a connection string in appsettings or environment variables");
            }
            builder.Services.AddDbContext<DentistAppointmentContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IUserBaseRepository, UserBaseRepository<DentistAppointmentContext>>();
            builder.Services.AddScoped<IUserRepository, UserRepository<DentistAppointmentContext>>();
            builder.Services.AddScoped<IDentalAppointmentRepository, DentalAppointmentRepository<DentistAppointmentContext>>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository<DentistAppointmentContext>>();
        }

        internal static void InitializeAppsettingsAndEnvironmentVariables(WebApplicationBuilder builder)
        {
            if (!builder.Environment.IsDevelopment())
            {
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

                builder.Configuration.AddEnvironmentVariables();

                //string keyVaultUri = builder.Configuration["KeyVaultUrl"];
                //builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
            }
        }

        internal static void InitilizeStorageServices(WebApplicationBuilder builder) //Use once azure is set up
        {
            var connectionString = builder.Configuration["Storage:ConnectionString"];
            var containerString = builder.Configuration["Storage:Container"];
            var publicConnectionString = builder.Configuration["Storage:PublicConnectionString"];
            var publicContainerString = builder.Configuration["Storage:PublicContainer"];
            if (!string.IsNullOrEmpty(connectionString))
            {
                builder.Services.AddSingleton(_ =>
                {
                    return new PrivateClient(new BlobServiceClient(connectionString).GetBlobContainerClient(containerString ?? "documents"));
                });

                if (!string.IsNullOrEmpty(publicConnectionString))
                {
                    builder.Services.AddSingleton(_ =>
                    {
                        return new PublicClient(new BlobServiceClient(publicConnectionString).GetBlobContainerClient(publicContainerString ?? "logos"));
                    });
                }
                //builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
                //builder.Services.AddScoped<IDocumentImplementation, DocumentImplementation>();
            }
        }

        internal static void InitializeBusinessLogic(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IDentalAppointmentService, DentalAppointmentService>();
        }

        internal static void InitializeServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            builder.Services.AddScoped<IAuthorizationHandler, UserContextHandler>();
        }
    }
}
