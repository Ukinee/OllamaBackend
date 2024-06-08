using Domain.Dto.Base;

namespace Domain.Dto.WebDtos.PostDtos
{
    public record PostMessageRequest : MessageBase
    {
        public Guid ConversationId { get; set; }
    }
}
