using DentistryAppointmentPrac.Shared.Documents.Interfaces;
using DentistryAppointmentPrac.Shared.Documents.Repository;
using DentistryAppointmentPrac.Shared.Models;
using DentistryAppointmentPrac.Shared.Users.BusinessLogic;
using DentistryAppointmentPrac.Shared.Utilities.Imports.Interface;
using DentistryAppointmentPrac.Shared.Utilities.Imports.Mappers;
using DentistryAppointmentPrac.Shared.Utilities.Imports.Validators;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.BusinessLogic
{
    public class UserImportHandler(IImportRepository repository, UserContext userContext,UserService userService, IDocumentRepository documentRepo, IDocumentService documentService) : IImportHandler
    {
        public ImportType ImportType => throw new NotImplementedException();

        public async Task HandleAsync(ImportProcess process)
        {
            switch (process.Status)
            {
                case ImportStatus.Pending:
                    {
                        await ProcessValidation(process);
                        break;
                    }
                case ImportStatus.Approved:
                case ImportStatus.Validated:
                    {
                        await ImportData(process);
                        break;
                    }
            }
        }
        
        private async Task ImportData(ImportProcess process)
        {
            //Apply data to DB
        }

        private async Task ProcessValidation(ImportProcess process)
        {
            try
            {
                var documentDetail = await documentService.GetDocument(process.DocumentId) ?? throw new ArgumentNullException("Document not found");

                var document = await documentService.StreamFile(process.DocumentId);
                if (document == null)
                {
                    process.Status = ImportStatus.Failed;
                    process.Errors?.Add($"Document {process.DocumentId} not found");
                    process.NumberOfErrors = (process.NumberOfErrors != null || process.NumberOfErrors > 0) ?
                        process.NumberOfErrors += process.Errors?.Count : process.Errors?.Count;
                    
                    await repository.ApplySaveChangesAsync();
                    return;
                }

                var records = new List<UserMapper>();
                if (userContext == null)
                    userContext = new UserContext();

                userContext.DentistDepartmentId = process.DepartmentId ?? Guid.Empty;
                userContext.UserId = process.UpdatedById.ToString() ?? "";
                userContext.Permissions = [PermissionEnum.Admin];
                userContext.TechSupportId = Guid.Empty.ToString();
                userContext.TechSupportUserName = "System Import";
                List<string> errors = new List<string>();

                if(documentDetail.DocumentName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    foreach(var batch in DataExtractionHelper.ExtractCsv<UserMapper>(document, process!.Mapping, process?.FileDelimiter))
                    {
                        errors = await ProcessEntries(process!, batch, errors);
                    }
                }
                else
                {
                    foreach(var batch in DataExtractionHelper.ExtractExcel<UserMapper>(document, process.Mapping))
                    {
                        errors = await ProcessEntries(process!, batch, errors);
                    }
                }

                if (errors.Any())
                {
                    process!.Errors = errors;
                    process.Status = ImportStatus.Failed;
                    process.NumberOfErrors = errors.Count;
                    repository.UpdateItemAsync(process);
                }
                else
                {
                    process!.Status = ImportStatus.Approved;
                    repository.UpdateItemAsync(process);
                }
                await repository.ApplySaveChangesAsync();
            }
            catch(Exception ex)
            {
                process.Errors = [ex.Message];
                process.Status = ImportStatus.Failed;
                process.NumberOfErrors = process.Errors.Count;

                repository.UpdateItemAsync(process);
                await repository.ApplySaveChangesAsync();
            }
        }

        private async Task<List<string>> ProcessEntries(ImportProcess process, List<UserMapper> records, List<string> errors)
        {
            if(!process.QuickImport || !process.PartialImport)
            {
                for(int i = 0; i < records.Count; i++)
                {
                    var item = records[i];

                    if (!string.IsNullOrEmpty(item.ID_Number))
                    {
                        var result = await new UserImportValidator().ValidateAsync(item);
                        if (result.Errors.Any())
                        {
                            string rowIssue = $" Row {i + 1} -";
                            foreach(var error in result.Errors)
                            {
                                rowIssue += $"{error.ErrorMessage.Replace("'", "")}";
                            }
                            errors.Add(rowIssue);
                        }
                    }
                }
            }
            return errors;
        }
    }
}
