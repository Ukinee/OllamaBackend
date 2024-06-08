using DataAccess.Interfaces;
using Domain.Models.Messages;
using Domain.Models.Messages.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EndPoints
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController(IMessageRepository messageRepository) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMessage([FromRoute] Guid id)
        {
            MessageEntity? message = await messageRepository.FindMessageByIdAsync(id);

            if (message == null)
                return NotFound();

            return Ok(message.ToGetMessageDto());
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageRequest messageRequest)
        {
            MessageEntity message = messageRequest.ToDatabaseMessage();

            await messageRepository.AddAsync(message);

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
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
