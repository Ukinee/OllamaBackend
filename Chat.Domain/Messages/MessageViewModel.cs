using Chat.Domain.Messages.Base;

namespace Chat.Domain.Messages
{
    public record MessageViewModel : MessageBase
    {
        public Guid SenderId { get; init; }
        public Guid Id { get; init; }
    }
}
