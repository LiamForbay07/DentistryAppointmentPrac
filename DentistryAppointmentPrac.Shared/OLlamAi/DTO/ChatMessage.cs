using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.DTO
{
    public class ChatMessage
    {
        public ChatRole Role { get; set; } //User | Assistant | System
        public string Content { get; set; } = string.Empty;
    }


}
