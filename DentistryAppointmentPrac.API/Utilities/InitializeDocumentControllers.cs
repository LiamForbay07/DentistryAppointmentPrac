using DentistryAppointmentPrac.Shared.Documents.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Scalar;
using Scalar.AspNetCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentistryAppointmentPrac.API.Utilities
{
    public static class InitializeDocumentControllers
    {
        public static void MapDocumentControllers(WebApplication app)
        {
            // Map document controllers here
            // Example:
            // app.MapControllerRoute(
            //     name: "documents",
            //     pattern: "api/documents/{action=Index}/{id?}",
            //     defaults: new { controller = "Documents" });
            app.MapGet("/api/documents/get-document/{id}", async ([FromQuery]Guid id, [FromServices]IDocumentService documentService) =>
            {
                var document = await documentService.GetDocument(id);
                return Results.Ok(document);
            }).WithName("GetDocumentById");
        }
    }
}
