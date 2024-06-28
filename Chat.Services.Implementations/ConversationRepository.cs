using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class ConversationRepository(UserDbContext userDbContext) : IConversationRepository
    {
        public async Task<List<ConversationEntity>> GetGeneralConversations(Guid userId)
        {
            return await userDbContext
                .Conversations
                .Where(conversationEntity => conversationEntity.OwnerId == userId)
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

        public async Task Update(PutConversationRequest request)
        {
            ConversationEntity? conversation = await userDbContext
                .Conversations
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (conversation == null)
            {
                throw new NotFoundException(nameof(conversation));
            }
            
            conversation.Name = request.Name;
            conversation.GlobalContext = request.GlobalContext;
            
            await Save();
        }

        private async Task Save()
        {
            await userDbContext.SaveChangesAsync();
        }
    }
}
