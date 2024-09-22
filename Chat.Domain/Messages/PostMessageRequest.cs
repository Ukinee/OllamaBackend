using Chat.Domain.Messages.Base;

namespace Chat.Domain.Messages
{
    public record PostMessageRequest : MessageBase
    {
        public Guid ConversationId { get; init; }
    }
}
