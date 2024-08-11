using Common.UserChatLinks.CQRS;
using Common.UserChatLinks.Models;
using Microsoft.AspNetCore.Mvc;

namespace Common.UserChatLinks.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPersonaToConversationRequest userChatLink)
        {
            await _addPersonaToConversationCommand.Execute(userChatLink);
            
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RemovePersonaFromConversationRequest userChatLink)
        {
            await _removePersonaFromConversationCommand.Execute(userChatLink);
            
            return Ok();
        }
    }
}
