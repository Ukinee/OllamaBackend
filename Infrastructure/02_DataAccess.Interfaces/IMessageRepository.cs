using Domain.Dto.DataBaseDtos;

namespace DataAccess.Interfaces
{
    public interface IMessageRepository
    {
        public Task<DatabaseMessageDto?> FindMessageByIdAsync(Guid id);
        public Task<List<DatabaseMessageDto>> FindMessagesByConversationAsync(Guid conversationId);
        
        public Task AddAsync(DatabaseMessageDto message);
        public Task RemoveAsync(DatabaseMessageDto message);
        public Task RemoveByOwnerAsync(Guid ownerId);
        
        public Task SaveAsync();
    }
}
