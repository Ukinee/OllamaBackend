using Chat.Domain.Conversations.Base;

namespace Chat.Domain.Conversations
{
    public record GeneralConversationViewModel : ConversationBase
    {
        public Guid Id { get; set; }
    }
}
