using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;
using Users.CQRS;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase //todo use cases
    {
        private readonly AddConversationQuery _addConversation;
        private readonly AddConversationToUserCommand _addConversationToUserCommand;
        private readonly DeleteConversationCommand _deleteConversationCommand;
        private readonly GetConcreteConversationQuery _getConcreteConversationQuery;

        public ConversationController
        (
            DeleteConversationCommand deleteConversationCommand,
            GetConcreteConversationQuery getConcreteConversationQuery,
            AddConversationQuery addConversation,
            AddConversationToUserCommand addConversationToUserCommand
        )
        {
            _deleteConversationCommand = deleteConversationCommand;
            _getConcreteConversationQuery = getConcreteConversationQuery;
            _addConversation = addConversation;
            _addConversationToUserCommand = addConversationToUserCommand;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id)
        {
            return await _getConcreteConversationQuery.Execute(this, id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest conversation)
        {
            Guid userId = User.GetGuid();

            GeneralConversationViewModel conversationViewModel = await _addConversation.Handle(conversation);
            await _addConversationToUserCommand.Execute(userId, conversationViewModel.Id);

            return Ok(conversationViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            return await _deleteConversationCommand.Execute(this, id);
        }
    }
}
