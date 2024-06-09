using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Mappers;

namespace Chat.CQRS.Queries
{
    public class AddConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public AddConversationQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<GeneralConversationViewModel> Handle(PostConversationRequest conversation, Guid userId)
        {
            ConversationEntity conversationEntity = conversation.ToEntity(userId);

            await _conversationRepository.Add(conversationEntity);

            return conversationEntity.ToGeneralConversation();
        }
    }
}
