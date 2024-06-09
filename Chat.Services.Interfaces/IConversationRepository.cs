using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities;

namespace Chat.Services.Interfaces
{
    public interface IConversationRepository
    {
        public Task<List<ConversationEntity>> GetGeneralConversations(Guid userId);
        
        public Task<ConversationEntity?> Get(Guid id);

        public Task Add(ConversationEntity conversation);
        public Task Delete(ConversationEntity conversation);
    }
}
