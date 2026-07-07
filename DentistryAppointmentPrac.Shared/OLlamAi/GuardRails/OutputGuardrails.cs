using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.GuardRails
{
    public static class OutputGuardrails
    {
        public static string Sanitize(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return response;
            }

            if(ContainsPromptLeak(response)) {

                return """
                        I cannot disclose internal instructions or system prompts.
                        How can I help you with dental appointments or services?
                       """;

            }
            return response;
        }

        private static bool ContainsPromptLeak(string response)
        {
            return response.Contains(
                "YOUR ROLE:",
                StringComparison.OrdinalIgnoreCase)
                ||
                response.Contains(
                "RESTRICTIONS:",
                StringComparison.OrdinalIgnoreCase)
                ||
                response.Contains(
                "SECURITY:",
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
