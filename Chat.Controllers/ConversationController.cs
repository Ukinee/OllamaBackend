using Chat.Domain.Conversations;
using Chat.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly ConversationsService _conversationsService;

        public ConversationController(ConversationsService conversationsService)
        {
            _conversationsService = conversationsService;
        }

        [HttpGet("{conversationId:guid}")]
        [HttpGet("{conversationId:guid}/messages/page/{routePage:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid conversationId, int? routePage, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int page = routePage ?? 1;
            
            ConversationViewModel result = await _conversationsService.
                GetPaginatedMessages(conversationId, page, 20, cancellationToken); // TODO: make configurable

            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostConversation
            ([FromBody] PostConversationRequest request, CancellationToken token)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            ConversationViewModel conversationViewModel = await _conversationsService.Add(request, token);

            return Ok(conversationViewModel);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutConversation([FromBody] PutConversationRequest request, CancellationToken token)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            ConversationViewModel viewModel = await _conversationsService.Update(request, token);

            return Ok(viewModel);
        }
    }
}
