using Chat.Domain.Conversations.Base;
using Chat.Domain.Messages;

namespace Chat.Domain.Conversations
{
    public record ConcreteConversationViewModel : ConversationBase
    {
        public required Guid Id { get; set; }
        public required List<Guid> PersonasId { get; set; }

        public required List<MessageViewModel> Messages { get; init; }
    }
}
