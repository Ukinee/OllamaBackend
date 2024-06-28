using Chat.Domain.Conversations.Base;

namespace Chat.Domain.Conversations
{
    public record PutConversationRequest : ConversationBase
    {
        public Guid Id { get; set; }
    }
}
