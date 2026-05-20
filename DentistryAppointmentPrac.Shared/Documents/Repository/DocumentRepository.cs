using DentistryAppointmentPrac.Shared.Documents.DTO;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Documents.Repository
{
    public class DocumentRepository<T>(T dbContext, UserContext userContext) : BaseWritableRepository<DocumentModel, DocumentSummary>(dbContext, userContext)
        where T: DbContext
    {
    }
}
