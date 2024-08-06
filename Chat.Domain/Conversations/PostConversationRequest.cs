using Chat.Domain.Conversations.Base;

namespace Chat.Domain.Conversations
{
    public record PostConversationRequest : ConversationBase
    {
        public required Guid PersonaId { get; set; }
    }
}
