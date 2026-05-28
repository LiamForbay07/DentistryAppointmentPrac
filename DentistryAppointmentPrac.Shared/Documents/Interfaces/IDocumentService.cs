using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.Documents.DTO;
using DentistryAppointmentPrac.Shared.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Documents.Interfaces
{
    public interface IDocumentService : IImplementation<DocumentModel, DocumentSummary, DocumentSummary>
    {
        Task<string> UploadFile(IFormFile file);
        Task<string> UploadQuickImport(IFormFile file);
        Task<string> DownloadFile(Guid id);
        Task<string> DownloadQuickImport(Guid documentId);
        Task<Stream?> StreamFile(Guid id);
        Task<DocumentModel?> GetDocument(Guid id);
        Task<DocumentModel?> GetQuickUploadDocument(Guid id);
    }
}
