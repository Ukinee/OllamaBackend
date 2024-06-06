using Domain.Dto.Base;

namespace Domain.Dto.WebDtos.GetDtos
{
    public record MessageViewModel : MessageBase
    {
        public Guid Id { get; set; }
    }
}
