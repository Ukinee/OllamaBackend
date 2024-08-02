﻿using Chat.CQRS.Queries;
using Chat.Domain.Conversations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Authorization.Common;

namespace Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly GetGeneralConversationsWithUserIdQuery _generalConversationsQuery;

        public ConversationsController(GetGeneralConversationsWithUserIdQuery generalConversationsQuery)
        {
            _generalConversationsQuery = generalConversationsQuery;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetGeneralConversations()
        {
            Guid userId = User.GetGuid();

            IList<GeneralConversationViewModel> conversations = await _generalConversationsQuery.Execute(userId);
            
            return Ok(conversations);
        }
    }
}
