using Chat.CQRS.Queries;
using Common.DataAccess.SharedEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly GetMessagesQuery _getMessages;

        public MessagesController(GetMessagesQuery getMessages)
        {
            _getMessages = getMessages;
        }

        [HttpGet("{conversationId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMessages([FromRoute] Guid conversationId)
        {
            Guid userId = User.GetGuid();

            IList<MessageEntity> messages = await _getMessages.Execute(conversationId, userId);

            return Ok(messages);
        }
    }
}
