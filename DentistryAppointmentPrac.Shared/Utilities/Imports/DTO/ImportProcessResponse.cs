using DentistryAppointmentPrac.Shared.Interfaces;
using DentistryAppointmentPrac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.DTO
{
    public class ImportProcessResponse(ImportProcess model) : ISummaryHandler<ImportProcess, ImportProcessResponse>, IResponseHandler<ImportProcess, ImportProcessResponse>
    {
        public Guid Id { get; set; } = model.Id;
        public DateTime? LastUpdated { get; set; } = model.LastUpdated;
        public string UpdatedById { get; set; } = model.UpdatedById.ToString() ?? "";
        public string UpdatedBy { get; set; } = model.UpdatedBy ?? "";
        public Guid DocumentId { get; set; } = model.DocumentId;
        public ImportStatus Status { get; set; } = model.Status;
        public List<string> Errors { get; set; } = model.Errors ?? [];
        public Dictionary<string, string> Mapping { get; set; } = model.Mapping;

        public static Expression<Func<ImportProcess, ImportProcessResponse>> SummaryMap => model => new ImportProcessResponse(model);

        public static ImportProcessResponse? FromEntity(ImportProcess model)
        {
            if (model == null) return null;
            return new ImportProcessResponse(model);
        }
    }
}
