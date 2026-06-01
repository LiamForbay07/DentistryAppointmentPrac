using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.DTO
{
    public class AiChatRequest
    {
        public List<ChatMessage> Messages { get; set; } = [];
    }
}
