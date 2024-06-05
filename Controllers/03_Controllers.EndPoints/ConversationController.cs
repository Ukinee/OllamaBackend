using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Domain.Dto.Extensions;
using Domain.Dto.WebDtos.PostDtos;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EndPoints
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConversationController(IConversationRepository conversationRepository, IMessageRepository messageRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetGeneralConversations()
        {
            List<DatabaseConversationDto> conversations = await conversationRepository.GetAllAsync();

            return Ok(conversations.Select(x => x.ToGeneralConversation()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id)
        {
            DatabaseConversationDto? conversation = await conversationRepository.FindConversationByIdAsync(id);

            if (conversation == null)
                return NotFound();

            return Ok(conversation.ToConcreteConversation());
        }

        [HttpPost]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationDto conversation)
        {
            DatabaseConversationDto databaseConversationDto = conversation.ToDatabaseConversation();

            await conversationRepository.AddAsync(databaseConversationDto);
            await conversationRepository.SaveAsync();

            return Ok(databaseConversationDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            DatabaseConversationDto? conversation = await conversationRepository.FindConversationByIdAsync(id);
            
            if (conversation == null)
                return NotFound();

            await messageRepository.RemoveByOwnerAsync(conversation.Id);
            await conversationRepository.RemoveAsync(conversation);
            
            await messageRepository.SaveAsync();
            await conversationRepository.SaveAsync();
            
            return NoContent();
        }
    }
}
