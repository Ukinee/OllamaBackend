using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Domain.Dto.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController(IConversationRepository conversationRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetGeneralConversations(CancellationToken cancellationToken)
        {
            List<ConversationEntity> conversations = await conversationRepository.GetAll(cancellationToken);

            return Ok(conversations.Select(x => x.ToGeneralConversation()));
        }
    }
}
