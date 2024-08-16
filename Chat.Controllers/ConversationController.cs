using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly GetConversationQuery _getConversationQuery;
        private readonly AddConversationQuery _addConversation;
        private readonly GetConversationViewModelQuery _getConversationViewModelQuery;
        private readonly UpdateConversationCommand _updateConversationCommand;

        public ConversationController
        (
            GetConversationQuery getConversationQuery,
            AddConversationQuery addConversation,
            GetConversationViewModelQuery getConversationViewModelQuery,
            UpdateConversationCommand updateConversationCommand
        )
        {
            _getConversationQuery = getConversationQuery;
            _addConversation = addConversation;
            _getConversationViewModelQuery = getConversationViewModelQuery;
            _updateConversationCommand = updateConversationCommand;
        }

        [HttpGet("{conversationId:guid}")]
        [HttpGet("{conversationId:guid}/messages/page/{routePage:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid conversationId, int routePage)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            ConcreteConversationViewModel result = await _getConversationViewModelQuery
                .Execute(conversationId, routePage, 20); // TODO: make configurable

            return Ok(result);
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

            return Ok(updatedConversation.ToGeneralConversationViewModel());
        }
    }
}
