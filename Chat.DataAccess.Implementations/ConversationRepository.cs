using System.Linq.Expressions;
using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Implementations
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly CompositeContext _userDbContext;

        public ConversationRepository(CompositeContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<ConversationEntity?> Find(Expression<Func<ConversationEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            ConversationEntity? conversationEntity = await _userDbContext
                .Conversations
                .Include(conversation => conversation.Personas)
                .Include(conversation => conversation.Messages)
                .ThenInclude(x => x.SenderPersona)
                .FirstOrDefaultAsync(predicate, cancellationToken);

            return conversationEntity;
        }

        public async Task<IEnumerable<ConversationEntity>> FindAll(Expression<Func<ConversationEntity, bool>> predicate)
        {
            return await _userDbContext
                .Conversations
                .Include(conversation => conversation.Personas)
                .Include(conversation => conversation.Messages)
                .ThenInclude(x => x.SenderPersona)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConversationEntity>> FindMany(int amount, Expression<Func<ConversationEntity, bool>> predicate)
        {
            return await _userDbContext
                .Conversations
                .Include(conversation => conversation.Personas)
                .Include(conversation => conversation.Messages)
                .ThenInclude(x => x.SenderPersona)
                .Where(predicate)
                .Take(amount)
                .ToListAsync();
        }

        public async Task Add(ConversationEntity conversation, CancellationToken token)
        {
            await _userDbContext.Conversations.AddAsync(conversation, token);

            await Save();
        }

        public async Task Save()
        {
            await _userDbContext.SaveChangesAsync();
        }
    }
}
