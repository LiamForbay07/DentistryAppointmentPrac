using Anthropic;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace DentistryAppointmentPrac.API.AnthropicServices.LLM
{
    public class ChatLLM
    {

        public async Task<ChatResponse> AIChatResponseService(string chatMessage)
        {
            // Configured using the ANTHROPIC_API_KEY, ANTHROPIC_AUTH_TOKEN and ANTHROPIC_BASE_URL environment variables
            AnthropicClient client = new();

            IChatClient chatClient = client.AsIChatClient("claud-opus-4-7");

            //Using McpClient from the MCP C# SDK:
            McpClient learningServer = await McpClient.CreateAsync(
                new HttpClientTransport(new() { Endpoint = new("https://learn.microsoft.com/api.mcp") }));

            ChatOptions options = new() { Tools = [.. await learningServer.ListToolsAsync()] };

            return await chatClient.GetResponseAsync(chatMessage);
        }
    }
}
