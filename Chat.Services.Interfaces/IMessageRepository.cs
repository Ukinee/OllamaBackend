using Chat.Domain.Messages;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;

namespace Chat.Services.Interfaces
{
    public interface IMessageRepository
    {
        public Task<MessageEntity?> Get(Guid id);
        public Task<List<MessageEntity>> Get(IList<Guid> messageIds);

        public Task Add(MessageEntity message);
        public Task Remove(MessageEntity message);
    }
}
