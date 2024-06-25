﻿using Chat.CQRS.Queries;
using Chat.Domain.Messages;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Mappers;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;

namespace Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController
    (
        CheckUserOwnsMessageQuery checkUserOwnsMessageQuery,
        GetMessageQuery getMessageQuery,
        AddMessageQuery addMessageQuery,
        DeleteMessageQuery deleteMessageQuery
    ) : ControllerBase
    {
        [HttpGet("{messageId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMessage([FromRoute] Guid messageId)
        {
            Guid userId = User.GetGuid();

            if (await checkUserOwnsMessageQuery.Execute(messageId, userId) == false)
                return Unauthorized();

            MessageEntity message = await getMessageQuery.Execute(messageId, userId);

            return Ok(message.ToViewModel());
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageRequest messageRequest)
        {
            Guid userId = User.GetGuid();

            MessageViewModel messageViewModel = await addMessageQuery.Handle(messageRequest, userId);

            return CreatedAtAction(nameof(GetMessage), new { id = messageViewModel.Id }, messageViewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage([FromQuery] Guid id)
        {
            Guid userId = User.GetGuid();

            if (await checkUserOwnsMessageQuery.Execute(id, userId) == false)
                return Unauthorized();

            await deleteMessageQuery.Remove(id);

            return NoContent();
        }
    }
}
