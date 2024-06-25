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
    public class ConversationController
    (
        DeleteConversationCommand deleteConversationCommand,
        GetConversationQuery getConversationQuery,
        AddConversationQuery addConversation,
        CheckUserOwnsConversationQuery checkUserOwnsConversationQuery,
        GetMessagesQuery getMessagesQuery
    )
        : ControllerBase
    {
        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id)
        {
            Guid userId = User.GetGuid();

            if (await checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();
            
            ConversationEntity conversation = await getConversationQuery.Execute(id);
            IList<MessageEntity> messages = await getMessagesQuery.Execute(id, userId);

            return Ok(conversation.ToConcreteConversation(messages));
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest conversation)
        {
            Guid userId = User.GetGuid();

            GeneralConversationViewModel conversationViewModel = await addConversation.Handle(conversation, userId);

            return Ok(conversationViewModel);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            Guid userId = User.GetGuid();

            if (await checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();

            await deleteConversationCommand.Execute(id);

            return NoContent();
        }
    }
}
