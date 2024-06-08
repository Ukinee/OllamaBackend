using Chat.Domain.Conversations;
using Chat.Domain.Conversations.Mappers;
using Chat.Services.Interfaces;

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
