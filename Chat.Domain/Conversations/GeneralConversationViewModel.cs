using Chat.Domain.Conversations.Base;

namespace Chat.Domain.Conversations
{
    public class GeneralConversationViewModel : ConversationBase
    {
        public required Guid Id { get; init; }
        
        public required Guid LastProcessedMessageId { get; init; }
    }
}
