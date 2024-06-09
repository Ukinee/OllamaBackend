using Chat.DataAccess;
using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class ConversationRepository(ChatDbContext chatDbContext) : IConversationRepository
    {
        public async Task<List<ConversationEntity>> GetGeneralConversations(Guid userId)
        {
            return await chatDbContext
                .Conversations
                .Where(conversationEntity => conversationEntity.OwnerId == userId)
                .ToListAsync();
        }

        public async Task<ConversationEntity?> Get(Guid id)
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
