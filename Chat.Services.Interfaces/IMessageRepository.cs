using Chat.Domain.Messages;

namespace Chat.Services.Interfaces
{
    public interface IMessageRepository
    {
        public Task<MessageEntity?> FindMessageByIdAsync(Guid id);
        public Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid conversationId);

        public Task Add(MessageEntity message);
        public Task RemoveAsync(MessageEntity message);
        public Task DeleteByConversationId(Guid id);
    }
}
