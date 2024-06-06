using Domain.Dto.Base;

namespace Domain.Dto.WebDtos.GetDtos
{
    public record ConcreteConversationViewModel : ConversationBase
    {
        public Guid Id { get; set; }

        public List<MessageViewModel> Messages { get; init; }
    }
}
