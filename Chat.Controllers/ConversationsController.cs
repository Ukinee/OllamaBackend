using Chat.Domain.Conversations;
using Chat.Domain.Conversations.Mappers;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConversationsController(IConversationRepository conversationRepository) : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetGeneralConversations(CancellationToken cancellationToken)
        {
            List<ConversationEntity> conversations = await conversationRepository.GetAll(cancellationToken);

            return Ok(conversations.Select(x => x.ToGeneralConversation()));
        }
    }
}
