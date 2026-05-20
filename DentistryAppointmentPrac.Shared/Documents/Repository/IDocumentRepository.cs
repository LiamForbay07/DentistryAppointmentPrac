using DentistryAppointmentPrac.Shared.Documents.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Documents.Repository
{
    public interface IDocumentRepository : IBaseWriteableRepository<DocumentModel, DocumentSummary>
    {
    }
}
