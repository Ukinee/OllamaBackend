using Chat.Domain.Messages.Base;

namespace Chat.Domain.Messages
{
    public record MessageViewModel : MessageBase
    {
        public required DateTime Timestamp { get; set; }
        public required Guid Id { get; init; }
    }
}
