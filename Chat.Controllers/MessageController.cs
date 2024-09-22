using Chat.Domain.Messages;
using Chat.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Obsolete("", true)]
    public class MessageController : ControllerBase
    {
        private readonly MessagesService _messagesService;

        public MessageController(MessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageRequest messageRequest, CancellationToken token)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            MessageViewModel messageViewModel = await _messagesService.AddMessage(messageRequest, token);

            return Ok(messageViewModel);
        }
    }
}
