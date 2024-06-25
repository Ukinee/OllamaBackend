using Common.DataAccess.SharedEntities.Objects;

namespace Chat.Services.Interfaces
{
    public interface IMessageRepository
    {
        public Task<MessageEntity?> Get(Guid id);
        public Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid[] messageIds);

        public Task Add(MessageEntity message);
        public Task Remove(MessageEntity message);
        public Task DeleteByConversationId(Guid id);
    }
}
