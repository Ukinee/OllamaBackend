using Domain.Dto.Base;

namespace Domain.Dto.DataBaseDtos
{
    public record ConversationEntity : ConversationBase
    {
        public Guid UserId { get; init; }
        
        public Guid Id { get; set; }

        public List<MessageEntity> Messages { get; init; } = new List<MessageEntity>();
    }
}
