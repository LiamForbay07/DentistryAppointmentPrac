using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Documents.DTO
{
    public class DocumentSummary : ISummaryHandler<DocumentModel, DocumentSummary>, IResponseHandler<DocumentModel, DocumentSummary>
    {
        public static Expression<Func<DocumentModel, DocumentSummary>> SummaryMap => throw new NotImplementedException();

        public static DocumentSummary? FromEntity(DocumentModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
