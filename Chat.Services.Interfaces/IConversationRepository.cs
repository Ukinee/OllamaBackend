using Chat.Domain.Conversations;

namespace Chat.Services.Interfaces
{
    public interface IConversationRepository
    {
        public Task<List<ConversationEntity>> GetAll(CancellationToken cancellationToken);
        public Task<ConversationEntity?> FindConversationById(Guid id);

        public Task Add(ConversationEntity conversation);
        public Task Delete(ConversationEntity conversation);
    }
}
