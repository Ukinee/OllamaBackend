using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class MessageRepository(ChatDbContext chatDbContext) : IMessageRepository
    {
        public async Task<MessageEntity?> FindMessageByIdAsync(Guid id) =>
            await chatDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(MessageEntity message) =>
            await chatDbContext.Messages.AddAsync(message);

        public Task RemoveAsync(MessageEntity message)
        {
            chatDbContext.Messages.Remove(message);

            return Task.CompletedTask;
        }

        public async Task RemoveByOwnerAsync(Guid ownerId)
        {
            List<MessageEntity> messages = await FindMessagesByConversationAsync(ownerId);
            chatDbContext.Messages.RemoveRange(messages);
        }

        public async Task SaveAsync() =>
            await chatDbContext.SaveChangesAsync();

        public async Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid conversationId)
        {
            return await chatDbContext
                .Messages
                .Where(x => x.ConversationDtoId == conversationId)
                .ToListAsync();
        }
    }
}
