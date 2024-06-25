using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class MessageRepository(UserDbContext userDbContext) : IMessageRepository
    {
        public Task<MessageEntity?> Get(Guid id)
        {
            return userDbContext
                .Messages
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MessageEntity>> Get(IList<Guid> messageIds)
        {
            return await userDbContext
                .Messages
                .Where(x => messageIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task Add(MessageEntity message)
        {
            await userDbContext.Messages.AddAsync(message);

            await Save();
        }

        public async Task Remove(MessageEntity message)
        {
            userDbContext.Messages.Remove(message);
            await Save();
        }

        public Task DeleteByConversationId(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task Save()
        {
            await userDbContext.SaveChangesAsync();
        }
    }
}
