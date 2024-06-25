using Chat.Domain.Conversations.Base;

namespace Common.DataAccess.SharedEntities
{
    public record ConversationEntity : ConversationBase
    {
        public Guid Id { get; init; }
        
        public Guid OwnerId { get; init; }
        public List<Guid> Participants { get; init; } = [];
        public List<Guid> Messages { get; init; } = [];
    }
}
