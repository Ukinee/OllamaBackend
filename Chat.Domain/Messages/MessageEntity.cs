using Domain.Models.Conversations;
using Domain.Models.Messages.Base;

namespace Domain.Models.Messages
{
    public record MessageEntity : MessageBase
    {
        public Guid Id { get; set; }
        
        public Guid? ConversationDtoId { get; set; }
        public ConversationEntity? ConversationDto { get; set; }
    }
}
