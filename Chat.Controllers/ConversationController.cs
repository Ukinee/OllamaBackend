using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Chats.Mappers;
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
        private readonly DeleteConversationCommand _deleteConversationCommand;
        private readonly GetConversationQuery _getConversationQuery;
        private readonly AddConversationQuery _addConversation;
        private readonly CheckUserOwnsConversationQuery _checkUserOwnsConversationQuery;
        private readonly GetMessagesQuery _getMessagesQuery;
        private readonly UpdateConversationCommand _updateConversationCommand;

        public ConversationController(DeleteConversationCommand deleteConversationCommand,
            GetConversationQuery getConversationQuery,
            AddConversationQuery addConversation,
            CheckUserOwnsConversationQuery checkUserOwnsConversationQuery,
            GetMessagesQuery getMessagesQuery,
            UpdateConversationCommand updateConversationCommand)
        {
            _deleteConversationCommand = deleteConversationCommand;
            _getConversationQuery = getConversationQuery;
            _addConversation = addConversation;
            _checkUserOwnsConversationQuery = checkUserOwnsConversationQuery;
            _getMessagesQuery = getMessagesQuery;
            _updateConversationCommand = updateConversationCommand;
        }

        [HttpGet("{id:guid}")]
        [HttpGet("{conversationId:guid}/messages/page/{routePage:int?}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id, int? routePage)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            if (await _checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();

            int page = routePage ?? 1; //todo: hardcode

            ConversationEntity conversation = await _getConversationQuery.Execute(id);
            IList<MessageEntity> messages = await _getMessagesQuery.Execute(id, userId, page, 20); //todo: hardcode

            return Ok(conversation.ToConcreteConversation(messages));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest conversation)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            GeneralConversationViewModel conversationViewModel = await _addConversation.Handle(conversation, userId);

            return Ok(conversationViewModel);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutConversation([FromBody] PutConversationRequest conversation)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            if (await _checkUserOwnsConversationQuery.Execute(conversation.Id, userId) == false)
                return Unauthorized($"{conversation.Id} does not belong to {userId}");

            await _updateConversationCommand.Execute(conversation);

            ConversationEntity updatedConversation = await _getConversationQuery.Execute(conversation.Id);

            return Ok(updatedConversation.ToGeneralConversation());
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            Guid userId = User.GetGuid();

            if (await _checkUserOwnsConversationQuery.Execute(id, userId) == false)
                return Unauthorized();

            await _deleteConversationCommand.Execute(id);

            return NoContent();
        }
    }
}
