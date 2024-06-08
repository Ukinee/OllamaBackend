using Domain.Models.Messages;

namespace DataAccess.Interfaces
{
    public interface IMessageRepository
    {
        public Task<MessageEntity?> FindMessageByIdAsync(Guid id);
        public Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid conversationId);
        
        public Task AddAsync(MessageEntity message);
        public Task RemoveAsync(MessageEntity message);
        public Task DeleteByConversationId(Guid id);
    }
}
