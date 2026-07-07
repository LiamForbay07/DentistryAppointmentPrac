using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic
{
    public interface IOLlamAiService
    {
        Task<string> GenerateResponse(AiUserRequest request);
        IAsyncEnumerable<string> GenerateLiveResponse(AiUserRequest request);
        IAsyncEnumerable<string> StreamChatAsync(List<DTO.ChatMessage> messages);
        IAsyncEnumerable<string> GenerateResponseFromModelWithHarness(List<DTO.ChatMessage> messages);
    }
}
