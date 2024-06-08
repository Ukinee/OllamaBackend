using Domain.Models.Conversations.Base;

namespace Domain.Models.Conversations
{
    public record GeneralConversationViewModel : ConversationBase
    {
        public Guid Id { get; set; }
    }
}
