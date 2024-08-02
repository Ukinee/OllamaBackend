using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries
{
    public class CheckUserParticipatesInConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public CheckUserParticipatesInConversationQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<bool> Execute(Guid conversationId, Guid userId)
        {
            ConversationEntity? conversationEntity = await _conversationRepository.Get(conversationId);

            if (conversationEntity == null)
                throw new NotFoundException(nameof(conversationEntity));

            return conversationEntity.OwnerId == userId || conversationEntity.Participants.Contains(userId);
        }
    }
}
