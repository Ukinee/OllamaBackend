using Domain.Dto.DataBaseDtos;

namespace DataAccess.Interfaces
{
    public interface IConversationRepository
    {
        public Task<List<ConversationEntity>> GetAll(CancellationToken cancellationToken);
        public Task<ConversationEntity?> FindConversationById(Guid id);
        
        public Task Add(ConversationEntity conversation);
        public Task Remove(ConversationEntity conversation);
    }
}
