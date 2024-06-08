using Authorization.Common;
using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Domain.Models.Conversations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.CQRS;

namespace Controllers.EndPoints
{
    [ApiController, Route("api/[controller]")]
    public class ConversationController : ControllerBase //todo use cases
    {
        private readonly DeleteConversationCommand _deleteConversationCommand;
        private readonly GetConcreteConversationQuery _getConcreteConversationQuery;
        private readonly AddConversationQuery _addConversation;
        private readonly AddConversationToUserCommand _addConversationToUserCommand;

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
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id) =>
            await _getConcreteConversationQuery.Execute(this, id);

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
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id) =>
            await _deleteConversationCommand.Execute(this, id);
    }
}
