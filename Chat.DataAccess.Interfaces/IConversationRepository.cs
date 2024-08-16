using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.DataAccess.Interfaces
{
    public interface IConversationRepository
    {
        public Task<ConversationEntity?> GetConcreteConversation(Guid conversationId);

        public Task Add(ConversationEntity conversation);
        public Task Delete(Guid id);
        
        public Task Update(PutConversationRequest request);
        public Task Update(ConversationEntity conversation);
    }
}
