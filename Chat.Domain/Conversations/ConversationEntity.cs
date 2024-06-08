using Chat.Domain.Conversations.Base;
using Chat.Domain.Messages;

namespace Chat.Domain.Conversations
{
    public record ConversationEntity : ConversationBase
    {
        public Guid UserId { get; init; }

        public Guid Id { get; set; }

        public List<MessageEntity> Messages { get; init; } = new List<MessageEntity>();
    }
}
