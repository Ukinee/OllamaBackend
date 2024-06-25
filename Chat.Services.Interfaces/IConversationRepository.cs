using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Objects;

namespace Chat.Services.Interfaces
{
    public interface IConversationRepository
    {
        public Task<List<ConversationEntity>> Get(Guid[] conversationIds);
        public Task<ConversationEntity?> Get(Guid id);

        public Task Add(ConversationEntity conversation);
        public Task Delete(ConversationEntity conversation);
    }
}
