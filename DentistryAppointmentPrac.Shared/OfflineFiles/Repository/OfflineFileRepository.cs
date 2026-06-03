using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.OfflineFiles.DTO;
using DentistryAppointmentPrac.Shared.OfflineFiles.Interfaces;
using DentistryAppointmentPrac.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OfflineFiles.Repository
{
    public class OfflineFileRepository<TContext>(TContext dbContext, UserContext userContext) : BaseWritableRepository<OfflineFile,OfflineFileSummaryResponse>(dbContext, userContext),IOfflineFileRepository
        where TContext : DbContext
    {
    }
}
