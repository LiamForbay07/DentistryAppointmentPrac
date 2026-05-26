using Azure.AI.Extensions.OpenAI;
using Azure.AI.Projects;
using Azure.AI.Projects.Agents;
using Azure.Identity;

namespace DentistryAppointmentPrac.API.MicrosoftAi.LLMServices
{
    public class MsAIClient
    {
        private readonly string _defaultModel = "gpt-5-mini";
        private readonly string _projectEndpoint = "your_project-endpoint";
        private readonly string _agentName = "";
        private const string AgentInstruction = "You are a helpful assistant that answers general questions";

        public AIProjectClient CreateProjectClient()
        {
            AIProjectClient projectClient = new(
                endpoint: new Uri(_projectEndpoint),
                tokenProvider: new DefaultAzureCredential());

            return projectClient;
        }
        
        public async Task<string?> CreateResponseAsync(AIProjectClient projectClient,string userInputText)
        {
            ProjectResponsesClient responseClient = projectClient.ProjectOpenAIClient.GetProjectResponsesClientForModel("gpt-5-mini");
            var response = await responseClient.CreateResponseAsync(userInputText);
            return response?.Value.GetOutputText() ?? string.Empty;
        }

        public async Task CreateAgentWithInstructions(AIProjectClient projectClient)
        {
            //Create an agent with a model and instructions:
            ProjectsAgentDefinition agentDefinition = new DeclarativeAgentDefinition(_defaultModel)
            {
                Instructions = AgentInstruction,
                Temperature = (float)0.3
            };

            ProjectsAgentVersion agent = await projectClient.AgentAdministrationClient.CreateAgentVersionAsync(
                agentName: _agentName,
                options: new(agentDefinition));

            Console.WriteLine($"Agent created (id: {agent.Id}, name: {agent.Name}, version: {agent.Version})");
        }

        public async Task<string> CreateConversationWithFollowUpResponse(AIProjectClient projectClient, ProjectConversation? conversation, string? followUpText)
        {
            conversation ??= await projectClient.ProjectOpenAIClient.GetProjectConversationsClient().CreateProjectConversationAsync();

            ProjectResponsesClient responseClient = projectClient.ProjectOpenAIClient.GetProjectResponsesClientForAgent(
                defaultAgent: _agentName,
                defaultConversationId: conversation.Id);

            var response = await responseClient.CreateResponseAsync(followUpText);
            return response?.Value.GetOutputText() ?? "No conversation found";
        }
    }
}
