using Domain.Models.Messages.Base;

namespace Domain.Models.Messages
{
    public record PostMessageRequest : MessageBase
    {
        public Guid ConversationId { get; set; }
    }
}
