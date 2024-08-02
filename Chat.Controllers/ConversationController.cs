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
        GetMessagesQuery getMessagesQuery,
        UpdateConversationCommand updateConversationCommand
    ) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        [HttpGet("{conversationId:guid}/messages/page/{routePage:int?}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id, int? routePage)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            if (await checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();

            int page = routePage ?? 1; //todo: hardcode

            ConversationEntity conversation = await getConversationQuery.Execute(id);
            IList<MessageEntity> messages = await getMessagesQuery.Execute(id, userId, page, 20); //todo: hardcode

            return Ok(conversation.ToConcreteConversation(messages));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest conversation)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            GeneralConversationViewModel conversationViewModel = await addConversation.Handle(conversation, userId);

            return Ok(conversationViewModel);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutConversation([FromBody] PutConversationRequest conversation)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            if (await checkUserOwnsConversationQuery.Execute(conversation.Id, userId) == false)
                return Unauthorized($"{conversation.Id} does not belong to {userId}");

            await updateConversationCommand.Execute(conversation);

            ConversationEntity updatedConversation = await getConversationQuery.Execute(conversation.Id);

            return Ok(updatedConversation.ToGeneralConversation());
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            if (await checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();

            await deleteConversationCommand.Execute(id);

            return NoContent();
        }
    }
}
