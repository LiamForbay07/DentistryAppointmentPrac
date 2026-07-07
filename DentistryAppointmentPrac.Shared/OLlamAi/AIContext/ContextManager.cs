using DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic.FineTuning;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.AIContext
{
    public static class ContextManager
    {
        public static List<ChatMessage> TrimMessages(List<ChatMessage> messages, int maxTokens = 2000)
        {
            var result = new List<ChatMessage>();
            int totalTokens = 0;

            foreach(var msg in messages.AsEnumerable().Reverse())
            {
                int estimatedTokens = msg.Content.Length / 4;

                if (totalTokens + estimatedTokens > maxTokens)
                    break;

                result.Insert(0, msg);

                totalTokens += estimatedTokens;
            }
            return result;
        }

        public static string BuildPrompt(List<ChatMessage> messages)
        {
            var sb = new StringBuilder();

            sb.AppendLine("[SYSTEM]");
            sb.AppendLine(SystemPrompts.DentalAssistant);

            sb.AppendLine();

            sb.AppendLine("[KNOWLEDGE]");
            sb.AppendLine(KnowledgeBase.DentalPractice);

            sb.AppendLine();

            foreach(var msg in messages)
            {
                string role = msg.Role.ToString().ToUpperInvariant();
                sb.AppendLine($"[{role}] {msg.Content}");
            }

            sb.AppendLine("[ASSISTANT]");
            return sb.ToString();
        }
    }
}
