using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.OfflineFiles.DTO;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OfflineFiles.Interfaces
{
    public interface IOfflineFileRepository : IBaseWriteableRepository<OfflineFile, OfflineFileSummaryResponse>
    {
    }
}
