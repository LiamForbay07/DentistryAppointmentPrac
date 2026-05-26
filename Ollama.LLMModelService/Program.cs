using OllamaSharp;
using OllamaSharp.Models.Chat;

namespace Ollama.LLMModelService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await SimpleOllamAIClient();
            Console.ReadLine();
        }

        internal static async Task SimpleOllamAIClient()
        {
            var ollama = new OllamaApiClient("http://localhost:11434", defaultModel: "gemma4:31b-cloud" /*defaultModel: "forbay07/chatbotmodel"*/);

            var message = new List<Message>()
            {
                new() {Role = "user", Content = "Why is C# so popular?"}
            };

            var chat = new Chat(ollama, ollama.SelectedModel);
            await foreach(var token in chat.SendAsync("Why is C# so popular?"))
            {
                Console.Write(token);
            }
            Console.WriteLine();

            //List models:
            var models = await ollama.ListLocalModelsAsync();
            foreach(var model in models)
            {
                Console.WriteLine($"{model.Name} - {model.Size / 1e9:F1} GB");
            }
        }
    }
}
