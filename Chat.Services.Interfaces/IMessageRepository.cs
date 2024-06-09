using Chat.Domain.Messages;
using Common.DataAccess.SharedEntities;

namespace Chat.Services.Interfaces
{
    public interface IMessageRepository
    {
        public Task<MessageEntity?> Get(Guid id);
        public Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid conversationId);

        public Task Add(MessageEntity message);
        public Task Remove(MessageEntity message);
        public Task DeleteByConversationId(Guid id);
    }
}
