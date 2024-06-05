using Domain.Dto.DataBaseDtos;

namespace DataAccess.Interfaces
{
    public interface IConversationRepository
    {
        public Task<List<DatabaseConversationDto>> GetAllAsync();
        public Task<DatabaseConversationDto?> FindConversationByIdAsync(Guid id);
        
        public Task AddAsync(DatabaseConversationDto conversation);
        public Task RemoveAsync(DatabaseConversationDto conversation);
        
        public Task SaveAsync();
    }
}
