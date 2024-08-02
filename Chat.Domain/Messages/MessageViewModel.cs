using Chat.Domain.Messages.Base;

namespace Chat.Domain.Messages
{
    public record MessageViewModel : MessageBase
    {
        public DateTime Timestamp { get; set; }
        public Guid SenderId { get; init; }
        
        public string ChatName { get; set; }
        public string ChatRole { get; set; }

        public Guid Id { get; init; }
    }
}
