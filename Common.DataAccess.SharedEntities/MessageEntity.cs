using Chat.Domain.Messages.Base;

namespace Common.DataAccess.SharedEntities
{
    public record MessageEntity : MessageBase
    {
        public Guid Id { get; init; }
        public Guid SenderId { get; init; }
        public DateTime Timestamp { get; set; }
        
        public string ChatName { get; set; }
        public string ChatRole { get; set; }

        public Guid ConversationId { get; init; }
    }
}
