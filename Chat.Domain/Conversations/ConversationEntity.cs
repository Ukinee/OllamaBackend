using Domain.Models.Conversations.Base;
using Domain.Models.Messages;

namespace Domain.Models.Conversations
{
    public record ConversationEntity : ConversationBase
    {
        public Guid UserId { get; init; }
        
        public Guid Id { get; set; }

        public List<MessageEntity> Messages { get; init; } = new List<MessageEntity>();
    }
}
