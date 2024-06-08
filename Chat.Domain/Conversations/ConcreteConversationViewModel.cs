using Domain.Models.Conversations.Base;
using Domain.Models.Messages;

namespace Domain.Models.Conversations
{
    public record ConcreteConversationViewModel : ConversationBase
    {
        public Guid Id { get; set; }

        public List<MessageViewModel> Messages { get; init; }
    }
}
