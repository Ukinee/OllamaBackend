using DataAccess.Interfaces;
using Domain.Models.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IMessageRepository messageRepository) : ControllerBase
    {
        [HttpGet("{conversationId:guid}")]
        public async Task<IActionResult> GetMessages([FromRoute] Guid conversationId)
        {
            List<MessageEntity> messages = await messageRepository.FindMessagesByConversationAsync(conversationId);
            
            return Ok(messages);
        }
    }
}
