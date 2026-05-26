using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Models.DbConfigurations
{
    public class RoomDetailDbConfiguration : IEntityTypeConfiguration<RoomDetail>
    {
        public void Configure(EntityTypeBuilder<RoomDetail> builder)
        {
            throw new NotImplementedException();
        }
    }
}
