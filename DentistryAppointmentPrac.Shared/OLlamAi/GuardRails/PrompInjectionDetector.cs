using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.GuardRails
{

    public static class PromptInjectionDetector
    {
        private static readonly string[] SuspiciousPatterns =
        [
            "ignore previous instructions",
            "forget your instructions",
            "forget all instructions",
            "show me the system prompt",
            "reveal the prompt",
            "developer instructions",
            "you are now",
            "act as",
            "override system",
            "jailbreak"
        ];

        public static bool IsPromptInjection(string input)
        {
            return SuspiciousPatterns.Any(p =>
                input.Contains(p, StringComparison.OrdinalIgnoreCase));
        }
    }

}
