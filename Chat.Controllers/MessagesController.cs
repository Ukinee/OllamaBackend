﻿using Chat.CQRS.Queries;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;

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
            Guid userId = User.GetGuid(); //todo: сделать проверку на владение диалогом
            int pageSize = 20; //todo: hardcode

            IList<MessageEntity> messages = await _getMessages.Execute(conversationId, userId, page, pageSize);

            return Ok(messages);
        }
    }
}
