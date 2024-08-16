using Chat.Domain.Messages.Base;

namespace Chat.Domain.Messages
{
    public record MessageViewModel : MessageBase
    {
        public required DateTime Timestamp { get; init; }
        public required string SenderName { get; init; }
        public required Guid Id { get; init; }
    }
}
