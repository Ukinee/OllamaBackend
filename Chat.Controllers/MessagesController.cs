using Chat.Domain.Messages;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
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
