using Chat.Domain.Conversations.Base;

namespace Chat.Domain.Conversations
{
    public class PostConversationRequest : ConversationBase
    {
        public required Guid PersonaId { get; set; }
    }
}
