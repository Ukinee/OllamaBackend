using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
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

        public async Task DeleteByConversationId(Guid ownerId)
        {
            List<MessageEntity> messages = await FindMessagesByConversationAsync(ownerId);
            userDbContext.Messages.RemoveRange(messages);

            await Save();
        }

        private async Task Save()
        {
            await userDbContext.SaveChangesAsync();
        }

        public async Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid conversationId)
        {
            return await userDbContext
                .Messages
                .Where(x => x.ConversationId == conversationId)
                .ToListAsync();
        }
    }
}
