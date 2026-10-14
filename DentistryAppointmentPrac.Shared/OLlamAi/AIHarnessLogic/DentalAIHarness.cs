using DentistryAppointmentPrac.Shared.OLlamAi.AIContext;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using DentistryAppointmentPrac.Shared.OLlamAi.GuardRails;
using OllamaSharp;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.AIHarnessLogic
{
    public sealed class DentalAIHarness(IOllamaApiClient client)
    {
        private readonly IOllamaApiClient _client = client;

        public async IAsyncEnumerable<string> ExecuteAsync(List<ChatMessage> messages)
        {
            if (messages.Count == 0)
                yield break;

            var latest = messages.LastOrDefault();

            if(latest != null)
            {
                InputGuardRails.Validate(latest.Content);

                if(PromptInjectionDetector.IsPromptInjection(latest.Content))
                {

                    yield return """
                                    I can assist with dental appointments and services,
                                    but I cannot comply with requests that attempt to
                                    modify or bypass my operating instructions.
                                    """;
                    yield break;
                }
                latest.Content = RoleGuard.Normalize(latest.Content);
            }

            var trimmed = ContextManager.TrimMessages(messages);

            var prompt = ContextManager.BuildPrompt(trimmed);

            StringBuilder responseBuilder = new();

            var chat = CreateNewChat();
            await foreach(var token in chat.SendAsync(prompt))
            {
                responseBuilder.Append(token);
            }

            string finalResponse = OutputGuardrails.Sanitize(responseBuilder.ToString());

            yield return finalResponse;
        }

        private Chat CreateNewChat() => new(_client, _client.SelectedModel);
        
    }
}
