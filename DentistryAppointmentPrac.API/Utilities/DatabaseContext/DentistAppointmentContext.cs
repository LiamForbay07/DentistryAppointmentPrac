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
        public DbSet<RoomDetails> RoomDetails { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<DocumentModel> Document { get; set; }
        public DbSet<ImportProcess> ImportProcess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new ImportProcessDbConfigurations());

            var userId = Guid.NewGuid();
            var dentistId = Guid.NewGuid();
            var roomDetailsId = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = userId,
                Name = "John",
                Surname = "Smith",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                LastUpdated = null,
                Password = PasswordHashHelper.HashPassword("Password@11"),
                Permissions = [PermissionEnum.Admin],
                Title = "Dr.",
                Username = "john@smtih@gmail.com",
                UpdatedBy = "System",
                UpdatedById = Guid.Empty,
                UserType = UserType.Dentist
            });

            modelBuilder.Entity<Dentist>().HasData(new Dentist()
            {
                Id = dentistId,
                Specialization = [DentalSpecializationEnum.General],
                CreatedDate = DateTime.Now,
                LastUpdated = null,
                IsDeleted = false,
                UpdatedBy = "System",
                UpdatedById = Guid.Empty,
                UserId = userId
            });

            modelBuilder.Entity<RoomDetails>().HasData(new RoomDetails()
            {
                Id = roomDetailsId,
                AvailableTimes = ["9:00-9:30", "9:30-10:00", "10:00-10:30", "10:30-11:00", "11:00-11:30", "13:00-13:30", "13:30-14:00"],
                Name = "Dental Room 1",
                Location = "North Wing Room 23",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                LastUpdated = null,
                UpdatedBy = "System",
                UpdatedById = Guid.Empty
            });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder); //Add DbConfigurations here
        }
    }
}
