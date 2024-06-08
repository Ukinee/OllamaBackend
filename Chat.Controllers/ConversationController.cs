using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Chat.Domain.Conversations.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;
using Users.CQRS;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly AddConversationQuery _addConversation;
        private readonly AddConversationToUserCommand _addConversationToUserCommand;
        private readonly DeleteConversationCommand _deleteConversationCommand;
        private readonly GetConversationQuery _getConversationQuery;

        public ConversationController
        (
            DeleteConversationCommand deleteConversationCommand,
            GetConversationQuery getConversationQuery,
            AddConversationQuery addConversation,
            AddConversationToUserCommand addConversationToUserCommand
        )
        {
            _deleteConversationCommand = deleteConversationCommand;
            _getConversationQuery = getConversationQuery;
            _addConversation = addConversation;
            _addConversationToUserCommand = addConversationToUserCommand;
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id)
        {
            ConversationEntity conversation = await _getConversationQuery.Execute(id);
            
            return Ok(conversation.ToConcreteConversation());
        }
        
        
        [HttpGet("[action]/{id:guid}")]
        // [Obsolete($"Probably you need to use {nameof(ConversationsController.GetGeneralConversations)} instead")]
        public async Task<IActionResult> GetGeneralConversation([FromRoute] Guid id)
        {
            ConversationEntity conversation = await _getConversationQuery.Execute(id);
            
            return Ok(conversation.ToGeneralConversation());
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest conversation)
        {
            Guid userId = User.GetGuid();

            GeneralConversationViewModel conversationViewModel = await _addConversation.Handle(conversation);
            await _addConversationToUserCommand.Execute(userId, conversationViewModel.Id);

            return Ok(conversationViewModel);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            throw new NotImplementedException();
            
            await _deleteConversationCommand.Execute(id);
            
            return NoContent();
        }
    }
}
