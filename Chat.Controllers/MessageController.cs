using Chat.CQRS.Queries;
using Chat.Domain.Messages;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Chats.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;

namespace Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly CheckUserOwnsMessageQuery _checkUserOwnsMessageQuery;
        private readonly GetMessageQuery _getMessageQuery;
        private readonly AddMessageQuery _addMessageQuery;
        private readonly DeleteMessageQuery _deleteMessageQuery;

        public MessageController(CheckUserOwnsMessageQuery checkUserOwnsMessageQuery,
            GetMessageQuery getMessageQuery,
            AddMessageQuery addMessageQuery,
            DeleteMessageQuery deleteMessageQuery)
        {
            _checkUserOwnsMessageQuery = checkUserOwnsMessageQuery;
            _getMessageQuery = getMessageQuery;
            _addMessageQuery = addMessageQuery;
            _deleteMessageQuery = deleteMessageQuery;
        }

        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMessage([FromRoute] Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Guid userId = User.GetGuid();

            if (await _checkUserOwnsMessageQuery.Execute(id, userId) == false)
                return Unauthorized();

            MessageEntity message = await _getMessageQuery.Execute(id);

            return Ok(message.ToViewModel());
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageRequest messageRequest)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Guid userId = User.GetGuid();

            MessageViewModel messageViewModel = await _addMessageQuery.Handle(messageRequest, userId);

            return CreatedAtAction(nameof(GetMessage), new { id = messageViewModel.Id }, messageViewModel);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteMessage([FromQuery] Guid id)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Guid userId = User.GetGuid();

            if (await _checkUserOwnsMessageQuery.Execute(id, userId) == false)
                return Unauthorized();

            await _deleteMessageQuery.Remove(id);

            return NoContent();
        }
    }
}
