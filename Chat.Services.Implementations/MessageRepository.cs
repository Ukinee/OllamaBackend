using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatContext _userDbContext;

        public MessageRepository(ChatContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public Task<MessageEntity?> Get(Guid id)
        {
            return _userDbContext
                .Messages
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MessageEntity>> Get(IList<Guid> messageIds)
        {
            try
            {
                return await _userDbContext
                    .Messages
                    .Where(x => messageIds.Contains(x.Id))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public async Task Add(MessageEntity message)
        {
            await _userDbContext.Messages.AddAsync(message);

            await Save();
        }

        public async Task Remove(MessageEntity message)
        {
            _userDbContext.Messages.Remove(message);
            await Save();
        }

        public Task DeleteByConversationId(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task Save()
        {
            await _userDbContext.SaveChangesAsync();
        }
    }
}
