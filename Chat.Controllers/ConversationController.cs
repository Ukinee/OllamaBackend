using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using ChatUserLink.CQRS;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Mappers;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly AddConversationQuery _addConversation;
        private readonly UserKnowAboutConversationQuery _checkUserOwnsConversationQuery;
        private readonly DeleteConversationCommand _deleteConversationCommand;
        private readonly GetConversationQuery _getConversationQuery;
        private readonly GetMessagesQuery _getMessagesQuery;

        public ConversationController
        (
            DeleteConversationCommand deleteConversationCommand,
            GetConversationQuery getConversationQuery,
            AddConversationQuery addConversation,
            UserKnowAboutConversationQuery checkUserOwnsConversationQuery,
            GetMessagesQuery getMessagesQuery
        )
        {
            _deleteConversationCommand = deleteConversationCommand;
            _getConversationQuery = getConversationQuery;
            _addConversation = addConversation;
            _checkUserOwnsConversationQuery = checkUserOwnsConversationQuery;
            _getMessagesQuery = getMessagesQuery;
        }

        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id)
        {
            Guid userId = User.GetGuid();

            if (await _checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();
            
            ConversationEntity conversation = await _getConversationQuery.Execute(id);
            IList<MessageEntity> messages = await _getMessagesQuery.Execute(id);

            return Ok(conversation.ToConcreteConversation(messages));
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest conversation)
        {
            Guid userId = User.GetGuid();

            GeneralConversationViewModel conversationViewModel = await _addConversation.Handle(conversation, userId);

            return Ok(conversationViewModel);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            Guid userId = User.GetGuid();

            if (await _checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();
            
            throw new NotImplementedException();

            await _deleteConversationCommand.Execute(id);

            return NoContent();
        }
    }
}
