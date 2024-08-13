using Common.UserChatLinks.CQRS;
using Common.UserChatLinks.Models;
using Microsoft.AspNetCore.Mvc;

namespace Common.UserChatLinks.Controllers
{
    [Route("api/[controller]/")]
    public class UserChatLinkController : ControllerBase
    {
        private readonly AddPersonaToConversationCommand _addPersonaToConversationCommand;
        private readonly RemovePersonaFromConversationCommand _removePersonaFromConversationCommand;

        public UserChatLinkController
        (
            AddPersonaToConversationCommand addPersonaToConversationCommand,
            RemovePersonaFromConversationCommand removePersonaFromConversationCommand
        )
        {
            _addPersonaToConversationCommand = addPersonaToConversationCommand;
            _removePersonaFromConversationCommand = removePersonaFromConversationCommand;
        }
        
        [HttpPost("link/{conversationId:guid}/{personaId:guid}")]
        public async Task<IActionResult> Post([FromRoute] Guid conversationId, [FromRoute] Guid personaId)
        {
            AddPersonaToConversationRequest userChatLink = new AddPersonaToConversationRequest
            {
                ConversationId = conversationId,
                PersonaId = personaId,
            };
            
            await _addPersonaToConversationCommand.Execute(userChatLink);
            
            return Ok();
        }

        [HttpDelete("unlink/{conversationId:guid}/{personaId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid conversationId, [FromRoute] Guid personaId)
        {
            RemovePersonaFromConversationRequest userChatLink = new RemovePersonaFromConversationRequest
            {
                ConversationId = conversationId,
                PersonaId = personaId,
            };
            
            await _removePersonaFromConversationCommand.Execute(userChatLink);
            
            return Ok();
        }
    }
}
