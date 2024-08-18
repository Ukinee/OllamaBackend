using Chat.CQRS.Queries;
using Chat.CQRS.Queries.Done;
using Chat.Domain.Messages;
using Chat.Services.Implementations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
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

        // [HttpGet("{id:guid}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<IActionResult> GetMessage([FromRoute] Guid id)
        // {
        //     if (ModelState.IsValid == false)
        //         return BadRequest(ModelState);
        //
        //     MessageEntity message = await _getMessageQuery.Execute(id);
        //
        //     return Ok(message.ToViewModel());
        // }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageRequest messageRequest, CancellationToken token)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            MessageViewModel messageViewModel = await _messagesService.AddMessage(messageRequest, token);

            return Ok(messageViewModel);
        }

        // [HttpDelete("{id:guid}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<IActionResult> DeleteMessage([FromQuery] Guid id)
        // {
        //     if (ModelState.IsValid == false)
        //         return BadRequest(ModelState);
        //
        //     await _deleteMessageQuery.Remove(id);
        //
        //     return NoContent();
        // }
    }
}
