using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models.DbConfigurations
{
    public class RoomDetailDbConfiguration : IEntityTypeConfiguration<RoomDetails>
    {
        public void Configure(EntityTypeBuilder<RoomDetails> builder)
        {
            throw new NotImplementedException();
        }
    }
}
