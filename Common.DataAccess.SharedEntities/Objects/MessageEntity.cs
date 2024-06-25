using Chat.Domain.Messages.Base;

namespace Common.DataAccess.SharedEntities.Objects
{
    public record MessageEntity : MessageBase
    {
        public Guid Id { get; init; }
        public Guid SenderId { get; init; }
        public DateTime Timestamp { get; set; }
    }
}
