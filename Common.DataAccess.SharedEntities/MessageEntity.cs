using Chat.Domain.Messages.Base;

namespace Common.DataAccess.SharedEntities
{
    public record MessageEntity : MessageBase
    {
        public Guid Id { get; init; }
        public Guid SenderId { get; init; }

        public Guid? ConversationId { get; init; }
        public ConversationEntity? Conversation { get; init; }
    }
}
