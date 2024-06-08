using Chat.DataAccess;
using Chat.Domain.Messages;
using Chat.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class MessageRepository(ChatDbContext chatDbContext) : IMessageRepository
    {
        public async Task<MessageEntity?> FindMessageByIdAsync(Guid id)
        {
            return await chatDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(MessageEntity message)
        {
            await chatDbContext.Messages.AddAsync(message);
        }

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

        private async Task Save()
        {
            await chatDbContext.SaveChangesAsync();
        }

        public async Task<List<MessageEntity>> FindMessagesByConversationAsync(Guid conversationId)
        {
            return await chatDbContext
                .Messages
                .Where(x => x.ConversationDtoId == conversationId)
                .ToListAsync();
        }
    }
}
