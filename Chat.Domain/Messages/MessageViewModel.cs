using Domain.Models.Messages.Base;

namespace Domain.Models.Messages
{
    public record MessageViewModel : MessageBase
    {
        public Guid Id { get; set; }
    }
}
