using Chat.Domain.Conversations.Base;

namespace Common.DataAccess.SharedEntities.Chats
{
    public record ConversationEntity : ConversationBase
    {
        public Guid Id { get; init; }
        
        public bool IsDeleted { get; set; }
        
        public Guid OwnerId { get; init; }
        public List<Guid> Participants { get; init; } = [];
        public List<Guid> Messages { get; set; } = [];
    }
}
