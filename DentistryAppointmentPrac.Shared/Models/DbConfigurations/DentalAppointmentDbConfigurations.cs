using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models.DbConfigurations
{
    public class DentalAppointmentDbConfigurations : IEntityTypeConfiguration<DentalAppointment>
    {
        public void Configure(EntityTypeBuilder<DentalAppointment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
