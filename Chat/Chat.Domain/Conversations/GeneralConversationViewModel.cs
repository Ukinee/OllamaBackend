using Domain.Dto.Base;

namespace Domain.Dto.WebDtos.GetDtos
{
    public record GeneralConversationViewModel : ConversationBase
    {
        public Guid Id { get; set; }
    }
}
