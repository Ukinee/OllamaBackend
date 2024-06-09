using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Mappers;
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
        private readonly CheckUserOwnsConversationQuery _checkUserOwnsConversationQuery;
        private readonly DeleteConversationCommand _deleteConversationCommand;
        private readonly GetConversationQuery _getConversationQuery;

        public ConversationController
        (
            DeleteConversationCommand deleteConversationCommand,
            GetConversationQuery getConversationQuery,
            AddConversationQuery addConversation,
            CheckUserOwnsConversationQuery checkUserOwnsConversationQuery
        )
        {
            _deleteConversationCommand = deleteConversationCommand;
            _getConversationQuery = getConversationQuery;
            _addConversation = addConversation;
            _checkUserOwnsConversationQuery = checkUserOwnsConversationQuery;
        }

        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id)
        {
            Guid userId = User.GetGuid();

            if (await _checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();
            
            ConversationEntity conversation = await _getConversationQuery.Execute(id);

            return Ok(conversation.ToConcreteConversation());
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

            await _deleteConversationCommand.Execute(id);

            return NoContent();
        }
    }
}
