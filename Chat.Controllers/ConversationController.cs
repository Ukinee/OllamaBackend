using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Chats.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly DeleteConversationCommand _deleteConversationCommand;
        private readonly GetConversationQuery _getConversationQuery;
        private readonly AddConversationQuery _addConversation;
        private readonly CheckUserInConversationQuery _checkUserInConversationQuery;
        private readonly GetMessagesQuery _getMessagesQuery;
        private readonly UpdateConversationCommand _updateConversationCommand;

        public ConversationController
        (
            DeleteConversationCommand deleteConversationCommand,
            GetConversationQuery getConversationQuery,
            AddConversationQuery addConversation,
            CheckUserInConversationQuery checkUserInConversationQuery,
            GetMessagesQuery getMessagesQuery,
            UpdateConversationCommand updateConversationCommand
        )
        {
            _deleteConversationCommand = deleteConversationCommand;
            _getConversationQuery = getConversationQuery;
            _addConversation = addConversation;
            _checkUserInConversationQuery = checkUserInConversationQuery;
            _getMessagesQuery = getMessagesQuery;
            _updateConversationCommand = updateConversationCommand;
        }

        [HttpGet("{id:guid}")]
        [HttpGet("{id:guid}/messages/page/{routePage:int?}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id, int? routePage)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int page = routePage ?? 1; //todo: hardcode

            ConversationEntity conversation = await _getConversationQuery.Execute(id);
            IList<MessageEntity> messages = await _getMessagesQuery.Execute(id, page, 20); //todo: hardcode

            return Ok(conversation.ToConcreteConversation(messages));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest request)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            GeneralConversationViewModel conversationViewModel = await _addConversation.Handle(request);

            return Ok(conversationViewModel);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutConversation([FromBody] PutConversationRequest conversation)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            await _updateConversationCommand.Execute(conversation);

            ConversationEntity updatedConversation = await _getConversationQuery.Execute(conversation.Id);

            return Ok(updatedConversation.ToGeneralConversation());
        }

        // [HttpDelete("{id:guid}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        // {
        //     if (ModelState.IsValid == false)
        //         return BadRequest(ModelState);
        //
        //     Guid userId = User.GetGuid();
        //
        //     if (await _checkUserOwnsConversationQuery.Execute(id, userId) == false)
        //         return Unauthorized();
        //
        //     await _deleteConversationCommand.Execute(id);
        //
        //     return NoContent();
        // }
    }
}
