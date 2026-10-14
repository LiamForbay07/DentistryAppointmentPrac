using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.DTO.DentalAIEvalHarnessDTO
{
    public class EvaluationResult
    {
        public string TestName { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public bool Passed { get; set; }
    }
}
