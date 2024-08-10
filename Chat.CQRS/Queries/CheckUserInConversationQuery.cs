using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries
{
    public class CheckUserInConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public CheckUserInConversationQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<bool> Execute(Guid conversationId, Guid userId)
        {
            ConversationEntity? conversationEntity = await _conversationRepository.GetConcreteConversation(conversationId);

            if (conversationEntity == null)
                throw new NotFoundException(nameof(conversationEntity));

            return conversationEntity.Personas.Any(x => x.UserId == userId);
        }
    }
}
