using Chat.Domain.Conversations.Base;
using Chat.Domain.Messages;

namespace Chat.Domain.Conversations
{
    public class ConversationViewModel : ConversationBase
    {
        public required Guid Id { get; init; }
        
        public required List<Guid> PersonasId { get; init; }
        public required List<MessageViewModel> Messages { get; init; }
    }
}
