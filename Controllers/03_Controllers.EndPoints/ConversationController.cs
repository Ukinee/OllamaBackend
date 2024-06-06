﻿using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Domain.Dto.Extensions;
using Domain.Dto.WebDtos.PostDtos;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.EndPoints
{
    [ApiController, Route("api/[controller]")]
    public class ConversationController //todo use cases
    (
        IConversationRepository conversationRepository,
        IMessageRepository messageRepository
    ) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetConcreteConversation([FromRoute] Guid id)
        {
            ConversationEntity? conversation = await conversationRepository.FindConversationById(id);

            if (conversation == null)
                return NotFound();

            return Ok(conversation.ToConcreteConversation());
        }

        [HttpPost]
        public async Task<IActionResult> PostConversation([FromBody] PostConversationRequest conversation)
        {
            ConversationEntity conversationEntity = conversation.ToDatabaseConversation();

            await conversationRepository.Add(conversationEntity);

            return CreatedAtAction(nameof(GetConcreteConversation), new { id = conversationEntity.Id }, conversationEntity);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteConversation([FromRoute] Guid id)
        {
            ConversationEntity? conversation = await conversationRepository.FindConversationById(id);

            if (conversation == null)
                return NotFound();

            await messageRepository.RemoveByOwnerAsync(conversation.Id);
            await conversationRepository.Remove(conversation);

            return NoContent();
        }
    }
}
