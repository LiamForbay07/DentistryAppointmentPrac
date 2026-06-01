using DentistryAppointmentPrac.Shared.OLlamAi.BusinessLogic;
using DentistryAppointmentPrac.Shared.OLlamAi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentistryAppointmentPrac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy ="MultiToken")]
    public class OLLamaAIModelApiController : ControllerBase
    {
        private readonly IOLlamAiService _olLamaAiService;

        public OLLamaAIModelApiController(IOLlamAiService olLamaAiService)
        {
            _olLamaAiService = olLamaAiService;
        }

        [HttpPost("generate-response")]
        public async Task<IActionResult> GenerateResponse([FromBody] /*AiUserRequest request*/ List<ChatMessage> request)
        {
            //var response = await _olLamaAiService.GenerateResponse(request);
            var response =  _olLamaAiService.StreamChatAsync(request);
            return Ok(new { Response = response });
        }


        [HttpPost("chat-stream")]
        public async Task ChatStream([FromBody] AiUserRequest request)
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

    }
}
