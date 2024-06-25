using ChatUserLink.CQRS;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(GetMessagesQuery getMessagesQuery) : ControllerBase
    {
        [HttpGet("{conversationId:guid}")]
        public async Task<IActionResult> GetMessages([FromRoute] Guid conversationId)
        {
            IList<MessageEntity> messages = await getMessagesQuery.Execute(conversationId);

            return Ok(messages);
        }
    }
}
