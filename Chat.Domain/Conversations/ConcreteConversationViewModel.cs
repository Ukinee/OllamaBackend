using Chat.Domain.Conversations.Base;
using Chat.Domain.Messages;

namespace Chat.Domain.Conversations
{
    public record ConcreteConversationViewModel : ConversationBase
    {
        public Guid Id { get; set; }

        public List<MessageViewModel> Messages { get; init; }
    }
}
