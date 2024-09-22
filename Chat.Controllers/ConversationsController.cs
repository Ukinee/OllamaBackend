using Chat.Domain.Conversations;
using Chat.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class ConversationsController : ControllerBase
    {
        private readonly ConversationsService _conversationsService;

        public ConversationsController(ConversationsService conversationsService)
        {
            _conversationsService = conversationsService;
        }

        [HttpGet("{personaId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetGeneralConversations([FromRoute] Guid personaId, CancellationToken token)
        {
            IList<GeneralConversationViewModel> conversations = await _conversationsService
                .GetGeneralConversationsByPersona(personaId, token);

            return Ok(conversations);
        }
    }
}
