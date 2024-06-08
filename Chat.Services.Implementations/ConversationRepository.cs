using Chat.DataAccess;
using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class ConversationRepository(ChatDbContext chatDbContext) : IConversationRepository
    {
        public async Task<List<ConversationEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await chatDbContext
                .Conversations
                .Include(dto => dto.Messages)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ConversationEntity>> GetGeneralConversations(IList<Guid> ids)
        {
            return await chatDbContext
                .Conversations
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<ConversationEntity?> FindConversationById(Guid id)
        {
            return await chatDbContext
                .Conversations
                .Include(dto => dto.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(ConversationEntity conversation)
        {
            await chatDbContext.Conversations.AddAsync(conversation);

            await Save();
        }

        public async Task Delete(ConversationEntity conversation)
        {
            chatDbContext.Conversations.Remove(conversation);

            await Save();
        }

        private async Task Save()
        {
            await chatDbContext.SaveChangesAsync();
        }
    }
}
