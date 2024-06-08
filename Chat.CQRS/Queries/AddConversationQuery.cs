using DataAccess.Interfaces;
using Domain.Models.Conversations;
using Domain.Models.Conversations.Mappers;

namespace Chat.CQRS.Queries
{
    public class AddConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public AddConversationQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<GeneralConversationViewModel> Handle(PostConversationRequest conversation)
        {
            ConversationEntity conversationEntity = conversation.ToEntity();

            await _conversationRepository.Add(conversationEntity);

            return conversationEntity.ToGeneralConversation();
        }
    }
}
