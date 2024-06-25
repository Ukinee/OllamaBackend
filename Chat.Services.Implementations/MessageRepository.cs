using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class MessageRepository(UserDbContext userDbContext) : IMessageRepository
    {
        public async Task<MessageEntity?> Get(Guid id)
        {
            return await userDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(MessageEntity message)
        {
            await userDbContext.Messages.AddAsync(message);
            
            await Save();
        }

        public Task Remove(MessageEntity message)
        {
            userDbContext.Messages.Remove(message);

            return Task.CompletedTask;
        }

        public Task DeleteByConversationId(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task Save()
        {
            await userDbContext.SaveChangesAsync();
        }

        public async Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid[] messageIds)
        {
            return await userDbContext
                .Messages
                .Where(x => messageIds.Contains(x.Id))
                .ToListAsync();
        }
    }
}
