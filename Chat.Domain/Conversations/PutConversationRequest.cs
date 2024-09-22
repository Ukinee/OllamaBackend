using Chat.Domain.Conversations.Base;

namespace Chat.Domain.Conversations
{
    public class PutConversationRequest : ConversationBase
    {
        public required Guid Id { get; init; }
        
        public Guid? LastProcessedMessageId { get; set; }
    }
}
