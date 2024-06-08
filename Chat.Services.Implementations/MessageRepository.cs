using DataAccess.Implementation;
using DataAccess.Interfaces;
using Domain.Models.Messages;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
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

        public async Task DeleteByConversationId(Guid ownerId)
        {
            List<MessageEntity> messages = await FindMessagesByConversationAsync(ownerId);
            chatDbContext.Messages.RemoveRange(messages);

            await Save();
        }

        private async Task Save() =>
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
