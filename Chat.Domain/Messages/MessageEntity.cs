using Chat.Domain.Conversations;
using Chat.Domain.Messages.Base;

namespace Chat.Domain.Messages
{
    public record MessageEntity : MessageBase
    {
        public Guid Id { get; set; }

        public Guid? ConversationDtoId { get; set; }
        public ConversationEntity? ConversationDto { get; set; }
    }
}
