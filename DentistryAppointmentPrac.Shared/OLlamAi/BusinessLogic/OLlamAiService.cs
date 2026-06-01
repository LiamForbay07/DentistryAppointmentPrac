using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OllamaSharp;
using OllamaSharp.Models.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic
{
    public class OLlamAiService : IOLlamAiService
    {
        private readonly OllamaApiClient _client;

        public OLlamAiService()
        {
            _client = new OllamaApiClient("http://localhost:11434",
                                           defaultModel: "gemma4:31b-cloud"
                                          /*defaultModel: "forbay07/chatbotmodel"*/
                                          );
        }

        public async IAsyncEnumerable<string> GenerateLiveResponse(AiUserRequest request)
        {
            if (string.IsNullOrEmpty(request.UserPrompt))
                throw new ArgumentException("User prompt cannot be null or empty.", nameof(request.UserPrompt));

            var chat = new Chat(_client, _client.SelectedModel);
            await foreach (var token in chat.SendAsync(request.UserPrompt))
            {
                yield return token;
            }
        }

        public async Task<string> GenerateResponse(AiUserRequest request)
        {
            if (string.IsNullOrEmpty(request.UserPrompt))
                throw new ArgumentException("User prompt cannot be null or empty.", nameof(request.UserPrompt));

            var chat = new Chat(_client, _client.SelectedModel);
            var response = "";
            await foreach (var token in chat.SendAsync(request.UserPrompt))
            {
                response += token;
            }
            ;

            return response;
        }

        public async IAsyncEnumerable<string> StreamChatAsync(List<ChatMessage> messages)
        {
            var chat = new Chat(_client, _client.SelectedModel);
            var trimmedMessages = TrimMessages(messages);
            var prompt = BuildStructuredPrompt(trimmedMessages);
            string response = "";
            await foreach (var token in chat.SendAsync(prompt))
            {
                response += token;
            }
            yield return response;
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

                result.Insert(0, msg); //Insert at the beginning to maintain order)
                totalTokens += estimatedTokens;
            }
            return result;
        }

        private string BuildPrompt(List<ChatMessage> messages)
        {
            var sb = ProvideLLMWithKnowldegeContext();
            var mostRecentMessages = messages.TakeLast(10); //Limit to last 10 messages for context
            foreach (var msg in mostRecentMessages)
            {
                sb.AppendLine($"{msg.Role}: {msg.Content}");
            }

            sb.AppendLine("Assistant:"); //Prompts model to respond as assistant
            return sb.ToString();
        }

        private string BuildStructuredPrompt(List<ChatMessage> messages)
        {
            var sb = ProvideLLMWithKnowldegeContext();
            var mostRecentMessages = messages.TakeLast(10); //Limit to last 10 messages for context

            foreach (var msg in messages)
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

        private static StringBuilder ProvideLLMWithKnowldegeContext()
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
}
