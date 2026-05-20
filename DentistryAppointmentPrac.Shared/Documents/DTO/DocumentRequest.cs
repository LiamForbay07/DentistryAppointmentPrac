using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Documents.DTO
{
    public class DocumentRequest : IRequestHandler<DocumentModel, DocumentRequest>
    {
        public static DocumentModel ToEntity(DocumentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
