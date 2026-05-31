using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Models.DbConfigurations;
using DentistryAppointmentPrac.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DentistryAppointmentPrac.API.Utilities.DatabaseContext
{
    public class DentistAppointmentContext(UserContext userContext, DbContextOptions<DentistAppointmentContext> options) : DbContext(options)
    {
        protected readonly UserContext _userContext = userContext;

        public DbSet<OfflineFile> OfflineFile { get; set; }
        public DbSet<DentalAppointment> DentalAppointment { get; set; }
        public DbSet<Dentist> Dentist { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<DocumentModel> Document { get; set; }
        public DbSet<ImportProcess> ImportProcess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DentistAppointmentContext).Assembly);
            modelBuilder.ApplyConfiguration(new ImportProcessDbConfigurations());
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = new Guid("36e4feae-93dc-4d48-bd40-2f7459caf773"),
                Name = "Liam",
                Surname = "Forbay",
                CreatedDate = new DateTime(2026, 5, 30, 13, 38, 36, 961, DateTimeKind.Utc),
                DisplayName = "Liam Forbay",
                IDNumber = "9707265099086",
                IsDeleted = false,
                LastUpdated = new DateTime(2026, 5, 30, 13, 38, 36, 961, DateTimeKind.Utc),
                Password = "um6PP5ICtRSHZ3xQ+jzG+OMU9g4EnJvLexx1nUXG9LMTwsM8Vs6N5De5h5tSX2hwmdIOR1a8OaubxgKd1iIgTQ==" , //Password@12
                UpdatedBy = "System",
                UpdatedById = null,
                UserType = UserType.MainUser,
                Permissions = [PermissionEnum.Admin, PermissionEnum.TechSupport],
                PasswordExpiryDate = null,
                Title = "Mr.",
                Username = "forbay07@gmail.com"
            });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<string>();
            base.ConfigureConventions(configurationBuilder); //Add DbConfigurations here
        }
    }
}
