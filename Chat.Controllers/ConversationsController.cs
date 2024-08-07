using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly GetGeneralConversationsWithUserIdQuery _generalConversationsQuery;

        public ConversationsController(GetGeneralConversationsWithUserIdQuery generalConversationsQuery)
        {
            _generalConversationsQuery = generalConversationsQuery;
        }

        [HttpGet("{personaId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetGeneralConversations([FromRoute] Guid personaId)
        {
            IList<GeneralConversationViewModel> conversations = await _generalConversationsQuery.Execute(personaId);

            Console.WriteLine($"Returned {conversations.Count} conversations");
            
            return Ok(conversations);
        }
    }
}
