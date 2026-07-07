using DentistryAppointmentPrac.Shared.OLlamAi.DTO;

namespace DentistryAppointmentPrac.Shared.OLlamAi.AIContext
{
    public sealed class OLlamaClientContext
    {
        public string SystemPrompt { get; set; } = "";
        public string Knowledge { get; set; } = "";
        public List<ChatMessage> Messages { get; set; } = [];
    }
}
