﻿using Chat.CQRS.Queries;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly GetMessagesQuery _getMessages;

        public MessagesController(GetMessagesQuery getMessages)
        {
            _getMessages = getMessages;
        }

        [HttpGet("{conversationId:guid}/messages")]
        [HttpGet("{conversationId:guid}/messages/page/{page:int?}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMessages([FromRoute] Guid conversationId, int page = 1)
        {
            int pageSize = 20; //todo: hardcode

            IList<MessageEntity> messages = await _getMessages.Execute(conversationId, page, pageSize);

            return Ok(messages.Select(x => x.ToViewModel()));
        }
    }
}
