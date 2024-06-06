using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class ConversationRepository(ChatDbContext chatDbContext) : IConversationRepository
    {
        public async Task<List<ConversationEntity>> GetAll(CancellationToken cancellationToken) =>
             await chatDbContext
                 .Conversations
                 .Include(dto => dto.Messages)
                 .ToListAsync(cancellationToken);

        public async Task<ConversationEntity?> FindConversationById(Guid id) =>
            await chatDbContext
                .Conversations
                .Include(dto => dto.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task Add(ConversationEntity conversation) =>
            await  chatDbContext.Conversations.AddAsync(conversation);

        public async Task Remove(ConversationEntity conversation) 
        {
            chatDbContext.Conversations.Remove(conversation);
            
            await Save();
        }

        public async Task Save() =>
            await chatDbContext.SaveChangesAsync();
    }
}
