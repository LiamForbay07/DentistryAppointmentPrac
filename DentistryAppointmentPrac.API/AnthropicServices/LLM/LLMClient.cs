using Anthropic;
using Anthropic.Models;
using Anthropic.Helpers;
using Anthropic.Models.Messages;
using System.Text.Json;
using Anthropic.Core;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.AI;
using Anthropic.Models.Beta.Files;
using System.Linq.Expressions;

namespace DentistryAppointmentPrac.API.AnthropicServices.LLM
{
    public class LLMClient 
    {
        public async Task<Message> CreateAnthropicLLMWithResponse()
        {
            using var client = new AnthropicClient();

            var response = await client.WithRawResponse.Messages.Create(new()
            {
                Model = Model.ClaudeHaiku4_5,
                MaxTokens = 1024,
                Messages = [
                            new() {Role = Role.User, Content = "Hello"},
                            new() { Role = Role.Assistant, Content="How Can I help you today?"}
                           ]
            });

            var content = await response.RawMessage.Content.ReadAsStringAsync();
            var deserializedContent = JsonConvert.DeserializeObject<Message>(content);

            return deserializedContent!;
        }

        public async Task CreateChatStream(AnthropicClient client, FileDownloadParams fileParams)
        {
            using var fileDownload = await client.Beta.Files.Download(fileParams);
            using var contentStream = await fileDownload.ReadAsStream();
            using var fileStream = File.Open("//define a path here", FileMode.OpenOrCreate);
            await contentStream.CopyToAsync(fileStream);

            var messageCreateParams = new MessageCreateParams(fileParams.RawHeaderData, fileParams.RawQueryData, fileParams.RawQueryData)
            {
                System = "Your role is an admin clerk who knows all about the doctor and dentists appointments going on, you should be able to help patience choose a medical professional based on their descriptions of illness.",
                Model = Model.ClaudeHaiku4_5,
                ServiceTier = ServiceTier.StandardOnly,
                MaxTokens = 1024,
                Messages = [new(){ Role = Role.User, Content = ""}]
            };
            var response = await client.WithRawResponse.Messages.Create(messageCreateParams);
        }
    }
}
