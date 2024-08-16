using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly CompositeContext _userDbContext;

        public MessageRepository(CompositeContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public Task<MessageEntity?> Get(Guid id)
        {
            return _userDbContext
                .Messages
                .Include(x => x.SenderPersona)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MessageEntity>> Get(IList<Guid> messageIds)
        {
            return await _userDbContext
                .Messages
                .Where(x => messageIds.Contains(x.Id))
                .ToListAsync();
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

        private async Task Save()
        {
            await _userDbContext.SaveChangesAsync();
        }
    }
}
