using Azure.Storage.Sas;
using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.Documents.DTO;
using DentistryAppointmentPrac.Shared.Documents.Interfaces;
using DentistryAppointmentPrac.Shared.Documents.Repository;
using DentistryAppointmentPrac.Shared.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Documents.BusinessLogic
{
    public class DocumentService(IDocumentRepository repository, PrivateClient privateClient, UserContext userContext, PublicClient? publicClient) : BaseWriteableImplementation<DocumentModel, DocumentSummary, DocumentSummary, DocumentRequest>(repository), IDocumentService
    {
        public async Task<string> DownloadFile(Guid id)
        {
            var document = await repository.GetItemByIdAsync(id);
            if (document == null) return "";

            var blobClient = privateClient.Client.GetBlobClient(document.DocumentEntry);
            if (!await blobClient.ExistsAsync())
                return "";

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = privateClient.Client.Name,
                BlobName = document.DocumentEntry,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-1),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1)
            };

            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);
            var sasUri = blobClient.GenerateSasUri(sasBuilder);
            return sasUri.ToString();
        }

        public Task<string> DownloadQuickImport(Guid id)
        {
            throw new NotImplementedException(); //From File System
        }

        public async Task<DocumentModel?> GetDocument(Guid id)
        {
            return await repository.GetItemByIdAsync(id);
        }

        public Task<DocumentModel?> GetQuickUploadDocument(Guid id)
        {
            throw new NotImplementedException(); //From File system
        }

        public Task<Stream?> StreamFile(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
