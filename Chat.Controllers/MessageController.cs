using Chat.Domain.Messages;
using Chat.Domain.Messages.Mappers;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController(IMessageRepository messageRepository) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")] // todo: hardcode
        public async Task<IActionResult> GetMessage([FromRoute] Guid id)
        {
            MessageEntity? message = await messageRepository.FindMessageByIdAsync(id);

            if (message == null)
                return NotFound();

            return Ok(message.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageRequest messageRequest)
        {
            MessageEntity message = messageRequest.ToEntity();

            await messageRepository.Add(message);

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message.ToViewModel());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage([FromQuery] Guid id)
        {
            MessageEntity? message = await messageRepository.FindMessageByIdAsync(id);

            if (message == null)
                return NotFound();

            await messageRepository.RemoveAsync(message);

            return NoContent();
        }
    }
}
