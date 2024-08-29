using Chat.Domain.Conversations.Base;

namespace Chat.Domain.Conversations
{
    public class PutConversationRequest : ConversationBase
    {
        public Guid Id { get; set; }
    }
}
