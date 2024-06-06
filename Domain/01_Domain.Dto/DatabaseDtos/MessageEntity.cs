using Domain.Dto.Base;

namespace Domain.Dto.DataBaseDtos
{
    public record MessageEntity : MessageBase
    {
        public Guid Id { get; set; }
        
        public Guid? ConversationDtoId { get; set; }
        public ConversationEntity? ConversationDto { get; set; }
    }
}
