using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.Interface
{
    public interface IImportHandler
    {
        ImportType ImportType { get; }
        Task HandleAsync(ImportProcess process);
    }
}
