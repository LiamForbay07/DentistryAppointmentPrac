using DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO.DentalAIEvalHarnessDTO;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.AIHarnessLogic
{
    public class DentalAIEvalHarness(IOLlamAiService ollamaAIService)
    {
        private readonly IOLlamAiService _ollamaAIService = ollamaAIService;

        public async Task<List<EvaluationResult>> RunAsync(List<EvalutationTestCase> tests)
        {
            var results = new List<EvaluationResult>();
            foreach (var test in tests)
            {
                List<DTO.ChatMessage> chatMessages =
                [
                    new DTO.ChatMessage
                    {
                        Role = "user",
                        Content = test.UserMessage
                    }
                ];
                var reponse = await GetFullResponseAsync(chatMessages);

                var passed = test.RequiredPhrases.All(phrase => reponse.Contains(phrase)) &&
                             !test.ForbiddenPhrases.Any(phrase => reponse.Contains(phrase));

                results.Add(new()
                {
                    TestName = test.Name,
                    Response = reponse,
                    Passed = passed
                });
            }
            return results;
        }

        public async Task<string> GetFullResponseAsync(List<ChatMessage> messages)
        {
            var sb = new StringBuilder();

            await foreach(var chunk in _ollamaAIService.GenerateResponseFromModelWithHarness(messages))
            {
                sb.Append(chunk);
            }
            return sb.ToString();
        }
    }
}
