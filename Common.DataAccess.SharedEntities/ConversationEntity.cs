using Chat.Domain.Conversations.Base;

namespace Common.DataAccess.SharedEntities
{
    public record ConversationEntity : ConversationBase
    {
        public Guid Id { get; set; }
        
        public UserEntity? Owner { get; init; }
        public Guid? OwnerId { get; init; }

        public List<MessageEntity> Messages { get; init; } = new List<MessageEntity>();
    }
}
