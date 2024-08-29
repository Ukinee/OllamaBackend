using Common.UserChatLinks.Services;
using Microsoft.AspNetCore.Mvc;

namespace Common.UserChatLinks.Controllers
{
    [Route("api/[controller]/")]
    public class UserChatLinkController : ControllerBase
    {
        private readonly UserChatLinkService _userChatLinkService;

        public UserChatLinkController(UserChatLinkService userChatLinkService)
        {
            _userChatLinkService = userChatLinkService;
        }

        [HttpPost("link/{conversationId:guid}/{personaId:guid}")]
        public async Task<IActionResult> Post
        (
            [FromRoute] Guid conversationId,
            [FromRoute] Guid personaId,
            CancellationToken cancellationToken
        )
        {
            await _userChatLinkService.AddPersona(conversationId, personaId, cancellationToken);

            return Ok();
        }

        [HttpDelete("unlink/{conversationId:guid}/{personaId:guid}")]
        public async Task<IActionResult> Delete
        (
            [FromRoute] Guid conversationId,
            [FromRoute] Guid personaId,
            CancellationToken cancellationToken
        )
        {
            await _userChatLinkService.RemovePersona(conversationId, personaId, cancellationToken);

            return Ok();
        }
    }
}
