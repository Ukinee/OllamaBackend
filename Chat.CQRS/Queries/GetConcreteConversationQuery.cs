using DataAccess.Interfaces;
using Domain.Models.Conversations;
using Domain.Models.Conversations.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Chat.CQRS.Queries
{
    public class GetConcreteConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public GetConcreteConversationQuery
        (
            IConversationRepository conversationRepository
        )
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<IActionResult> Execute(ControllerBase controller, Guid id)
        {
            ConversationEntity? conversation = await _conversationRepository.FindConversationById(id);

            if (conversation == null)
                return controller.NotFound();

            return controller.Ok(conversation.ToConcreteConversation());
        }
    }
}
