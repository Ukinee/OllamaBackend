using System.Security.Claims;
using System.Security.Principal;
using DataAccess.Interfaces;
using Domain.Models.Conversations;
using Domain.Models.Conversations.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Controllers.EndPoints;

[Route("api/[controller]/[action]")]
[ApiController]
public class ConversationsController(IConversationRepository conversationRepository, ILogger<ConversationsController> logger)
    : ControllerBase
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetGeneralConversations(CancellationToken cancellationToken)
    {
        List<ConversationEntity> conversations = await conversationRepository.GetAll(cancellationToken);

        return Ok(conversations.Select(x => x.ToGeneralConversation()));
    }
}