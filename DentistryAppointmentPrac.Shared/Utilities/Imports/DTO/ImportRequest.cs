using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.DTO
{
    public class ImportRequest : IRequestHandler<ImportProcess, ImportRequest>
    {
        [JsonRequired]
        public Guid DocumentId { get; set; }
        public Dictionary<string, string> Mapping { get; set; } = [];
        public char? SelectedFileDelimiter { get; set; }
        public bool QuickImport { get; set; } = false; //skips queue process
        public bool PartialImport { get; set; } = false;
        public static ImportProcess ToEntity(ImportRequest request)
        {
            if (request == null) return new();
            return new ImportProcess
            {
                Id = Guid.NewGuid(),
                DocumentId = request.DocumentId,
                Mapping = request.Mapping,
                Status = request.QuickImport ? ImportStatus.QuickImport : ImportStatus.Pending,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                FileDelimiter = request.SelectedFileDelimiter,
                PartialImport = request.PartialImport,
                QuickImport = request.QuickImport
            };
        }
    }
}
