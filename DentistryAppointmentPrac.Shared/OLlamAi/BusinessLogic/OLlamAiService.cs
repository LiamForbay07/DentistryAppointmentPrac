using DentistryAppointmentPrac.Shared.OLlamAi.AIHarnessLogic;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OllamaSharp;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic
{
    public class OLlamAiService : IOLlamAiService
    {
        private readonly OllamaApiClient _client;
        private readonly DentalAIHarness _harness;

        public OLlamAiService(DentalAIHarness harness, IConfiguration configuration)
        {
            var defaultModel = configuration["OllamaApiClient:defaultModel"] ?? string.Empty;

            var baseUrl = configuration.GetValue<string>("OllamaApiClient:BaseUrl")
                          ?? Environment.GetEnvironmentVariable("OllamaApiClient:BaseUrl")
                          ?? throw new ArgumentException("OllamaApiClient:BaseUrl must be configured via configuration or OLLAMA_BASE_URL.");

            _client = new OllamaApiClient(baseUrl, defaultModel: defaultModel);
            _harness = harness;
        }

        public async IAsyncEnumerable<string> GenerateResponseFromModelWithHarness(List<DTO.ChatMessage> messages)
        {
            await foreach(var response in _harness.ExecuteAsync(messages))
            {
                yield return response;
            }
        }

        public async IAsyncEnumerable<string> GenerateLiveResponse(AiUserRequest request)
        {
            if (string.IsNullOrEmpty(request.UserPrompt))
                yield return "User prompt cannot be null or empty.";

            var chat = new Chat(_client, _client.SelectedModel);
            await foreach (var token in chat.SendAsync(request.UserPrompt))
            {
                yield return token;
            }
        }

        public async Task<string> GenerateResponse(AiUserRequest request)
        {
            if (string.IsNullOrEmpty(request.UserPrompt))
                throw new ArgumentException("User prompt cannot be null or empty.");

            var chat = new Chat(_client, _client.SelectedModel);
            var response = new StringBuilder();
            await foreach (var token in chat.SendAsync(request.UserPrompt))
            {
                response.Append(token);
            }
            return response.ToString();
        }

        public async IAsyncEnumerable<string> StreamChatAsync(List<ChatMessage> messages)
        {
            var chat = new Chat(_client, _client.SelectedModel);
            var trimmedMessages = TrimMessages(messages);
            var prompt = BuildStructuredPrompt(trimmedMessages);
            var response = new StringBuilder();
            await foreach (var token in chat.SendAsync(prompt))
            {
                response.Append(token);
            }
            yield return response.ToString();
        }

        private static List<ChatMessage> TrimMessages(List<ChatMessage> messages, int maxTokens = 2000)
        {
            var result = new List<ChatMessage>();
            var totalTokens = 0;

            foreach(var msg in messages.AsEnumerable().Reverse())
            {
                var estimatedTokens = msg.Content.Length / 4; //Rough estimate: 1 token ~ 4 characters
                if (totalTokens + estimatedTokens > maxTokens)
                    break;

                result.Insert(0, msg); //Insert at the beginning to maintain order
                totalTokens += estimatedTokens;
            }
            return result;
        }

        private static string BuildStructuredPrompt(List<ChatMessage> messages)
        {
            var sb = ProvideLLMWithSystemPromptInstructions();
            var mostRecentMessages = messages.TakeLast(10); //Limit to last 10 messages for context

            foreach (var msg in mostRecentMessages)
            {
                switch (msg.Role.ToString().ToLower())
                {
                    case "user":
                        sb.AppendLine($"[USER]: {msg.Content}");
                        break;
                    case "assistant":
                        sb.AppendLine($"[ASSISTANT]: {msg.Content}");
                        break;
                    case "system": sb.AppendLine($"[SYSTEM]: {msg.Content}"); break;
                }
            }

            sb.AppendLine("[ASSISTANT]:"); //Prompt model to respond as assistant
            return sb.ToString();

        }

        private static StringBuilder ProvideLLMWithSystemPromptInstructions()
        {
            var sb = new StringBuilder();
            sb.AppendLine("You are a helpful assistant for a dental appointment scheduling application.");
            sb.AppendLine("You can answer questions about available appointment times, services offered, and general information about the dental practice.");
            sb.AppendLine("Use the following information to assist users:");
            sb.AppendLine("- The dental practice is open Monday to Friday from 8am to 5pm.");
            sb.AppendLine("- We offer general dentistry, orthodontics, and cosmetic dentistry services.");
            sb.AppendLine("- To schedule an appointment, please provide your name, preferred date and time, and the type of service you need.");
            return sb;
        }
    }

    public class OllamaOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string DefaultModel { get; set; } = string.Empty;
    }
}
