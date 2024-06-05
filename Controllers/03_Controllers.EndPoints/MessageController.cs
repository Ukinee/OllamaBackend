using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Domain.Dto.Extensions;
using Domain.Dto.WebDtos.PostDtos;
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
            DatabaseMessageDto? message = await messageRepository.FindMessageByIdAsync(id);

            if (message == null)
                return NotFound();

            return Ok(message.ToGetMessageDto());
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageDto message)
        {
            DatabaseMessageDto databaseMessage = message.ToDatabaseMessage();

            await messageRepository.AddAsync(databaseMessage);
            await messageRepository.SaveAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = databaseMessage.Id }, databaseMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage([FromQuery] Guid id)
        {
            DatabaseMessageDto? message = await messageRepository.FindMessageByIdAsync(id);
            
            if (message == null)
                return NotFound();
            
            await messageRepository.RemoveAsync(message);
            
            return NoContent();
        }
    }
}
