using DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentistryAppointmentPrac.API.Controllers
{
    [Route("ollama-llm")]
    [ApiController]
    [Authorize(Policy ="MultiToken")]
    public class OLLamaAIModelApiController : ControllerBase
    {
        private readonly IOLlamAiService _olLamaAiService;

        public OLLamaAIModelApiController(IOLlamAiService olLamaAiService)
        {
            _olLamaAiService = olLamaAiService;
        }

        //[HttpPost("generate-response-with-system-prompt")] //Trained model
        //public async Task<IActionResult> GenerateResponse([FromBody] List<ChatMessage> request)
        //{
        //    var response =  _olLamaAiService.StreamChatAsync(request);
        //    return Ok(new { Response = response });
        //}


        [HttpPost("generate-untrained-response")]
        public async Task ChatStream([FromBody] AiUserRequest request) //Untrained model
        {
            Response.ContentType = "text/event-stream";
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("X-Accel-Buffering", "no");

            await foreach (var token in _olLamaAiService.GenerateLiveResponse(request))
            {
                await Response.WriteAsync(token);
                await Response.Body.FlushAsync();
            }
        }

        //For model that uses harness:
        [HttpPost("generate-response-with-harness")]
        public async Task<IActionResult> GenerateResponseFromModelWithHarness([FromBody] List<ChatMessage> messages)
        {
            var response = _olLamaAiService.GenerateResponseFromModelWithHarness(messages);
            return Ok(new { Response = response });
        }
    }
}
