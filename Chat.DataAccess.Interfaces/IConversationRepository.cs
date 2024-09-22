using System.Linq.Expressions;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.DataAccess.Interfaces
{
    public interface IConversationRepository
    {
        public Task<ConversationEntity?> Find(Expression<Func<ConversationEntity, bool>> predicate, CancellationToken cancellationToken);
        public Task<IEnumerable<ConversationEntity>> FindAll(Expression<Func<ConversationEntity, bool>> predicate);
        public Task<IEnumerable<ConversationEntity>> FindMany(int amount, Expression<Func<ConversationEntity, bool>> predicate);
        
        public Task Add(ConversationEntity conversation, CancellationToken token);
        public Task Save();
    }
}
