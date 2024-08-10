using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;

namespace Chat.Services.Interfaces
{
    public interface IConversationRepository
    {
        public Task<ConversationEntity?> GetConcreteConversation(Guid id);

        public Task Add(ConversationEntity conversation);
        public Task Delete(Guid id);
        public Task Update(PutConversationRequest request);
    }
}
