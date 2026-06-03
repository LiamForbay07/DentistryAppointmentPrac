using System.Net;

namespace DentistryAppointmentPrac.Shared.Models
{
    public class IdempotencyRecord
    {
        public Guid Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string RequestHash { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
