using DentistryAppointmentPrac.Shared.OLlamAi.AIHarnessLogic;
using DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using Moq;

namespace DentistryAppointmentPrac.Tests
{
    public class EvalHarnessTests
    {
        [Fact]
        public async Task DentalAi_Should_Pass_Evaluation() 
        {
            var result = ""; 
        }

        private static DentalAIEvalHarness CreateHarness()
        {
            var mockAIService = new Mock<IOLlamAiService>();
            return new DentalAIEvalHarness(mockAIService.Object);
        }
    }
}
