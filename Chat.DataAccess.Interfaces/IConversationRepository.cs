using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.DataAccess.Interfaces
{
    public interface IConversationRepository
    {
        public Task<ConversationEntity?> Find(Func<ConversationEntity, bool> predicate, CancellationToken cancellationToken);
        public Task<IEnumerable<ConversationEntity>> FindAll(Func<ConversationEntity, bool> predicate);
        public Task<IEnumerable<ConversationEntity>> FindMany(int amount, Func<ConversationEntity, bool> predicate);
        
        public Task Add(ConversationEntity conversation, CancellationToken token);
        public Task Save();
    }
}
