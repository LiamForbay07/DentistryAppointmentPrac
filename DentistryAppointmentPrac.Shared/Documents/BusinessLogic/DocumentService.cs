using Azure.Storage.Sas;
using DentistryAppointmentPrac.Shared.BusinessLogic;
using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using DentistryAppointmentPrac.Shared.Documents.DTO;
using DentistryAppointmentPrac.Shared.Documents.Interfaces;
using DentistryAppointmentPrac.Shared.Documents.Repository;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.OfflineFiles.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Documents.BusinessLogic
{
    //Can do online and offline files
    public class DocumentService(IDocumentRepository repository,IOfflineFileRepository offlineFileRepo, /*PrivateClient privateClient,*/ UserContext userContext/*, PublicClient? publicClient*/) : BaseWriteableImplementation<DocumentModel, DocumentSummary, DocumentSummary, DocumentRequest>(repository), IDocumentService
    {
        public async Task<string> DownloadFile(Guid id)
        {
            var document = await repository.GetItemByIdAsync(id);
            if (document == null) return "";

            //var blobClient = privateClient.Client.GetBlobClient(document.DocumentEntry);
            //if (!await blobClient.ExistsAsync())
            //    return "";

            var sasBuilder = new BlobSasBuilder
            {
                //BlobContainerName = privateClient.Client.Name,
                BlobName = document.DocumentEntry,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-1),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1)
            };

            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);
            //var sasUri = blobClient.GenerateSasUri(sasBuilder);
            //return sasUri.ToString();
            return "";
        }

        public async Task<string> DownloadQuickImport(Guid documentId)
        {
            var offlineDocument = await offlineFileRepo.FindSingleItemByFilterAsync(x => x.DocumentId == documentId) ??
                                  throw new ArgumentNullException("Document no found");

            var filePath = Directory.CreateDirectory(Directory.GetCurrentDirectory()).ToString();
            
            var fullFilePath = Path.Combine(filePath, offlineDocument.FileName);
            File.WriteAllBytes(fullFilePath, offlineDocument.Data);

            return fullFilePath;
        }
        
        public async Task<string> UploadQuickImport(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var document = new DocumentModel
            {
                Id = Guid.NewGuid(),
                ContentType = file.ContentType,
                FileSize = file.Length,
                CreatedDate = DateTime.UtcNow,
                DocumentEntry = file.FileName + "LocalEntry",
                DocumentName = file.FileName,
                HasOfflineBackup = true,
                UpdatedBy = userContext.Username,
                UpdatedById = Guid.Parse(userContext.UserId)
            };

            var fileRecord = new OfflineFile
            {
                FileName = file.FileName,
                FileSize = file.Length,
                CreatedDate = DateTime.UtcNow,
                Data = ms.ToArray(),
                UpdatedBy = userContext.Username,
                UpdatedById = Guid.Parse(userContext.UserId),
                DocumentId = document.Id 
            };

            repository.AddItemAsync(document);
            await repository.ApplySaveChangesAsync();

            offlineFileRepo.AddItemAsync(fileRecord);

            var result = await offlineFileRepo.ApplySaveChangesAsync() > 0;
            return result ? "Successfully uploaded file" : "Failed to upload file";
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
