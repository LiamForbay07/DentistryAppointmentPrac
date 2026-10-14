using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.DTO.DentalAIEvalHarnessDTO
{
    public class EvalutationTestCase
    {
        public string Name { get; set; } = string.Empty;
        public string UserMessage { get; set; } = string.Empty;
        public List<string> RequiredPhrases { get; set; } = [];
        public List<string> ForbiddenPhrases { get; set; } = [];
    }
}
