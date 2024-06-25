using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class ConversationRepository(UserDbContext userDbContext) : IConversationRepository
    {
        public async Task<List<ConversationEntity>> Get(Guid[] conversationIds)
        {
            return await userDbContext
                .Conversations
                .Where(x => conversationIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<ConversationEntity?> Get(Guid id)
        {
            return await userDbContext
                .Conversations
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(ConversationEntity conversation)
        {
            await userDbContext.Conversations.AddAsync(conversation);

            await Save();
        }

        public async Task Delete(ConversationEntity conversation)
        {
            userDbContext.Conversations.Remove(conversation);

            await Save();
        }

        private async Task Save()
        {
            await userDbContext.SaveChangesAsync();
        }
    }
}
