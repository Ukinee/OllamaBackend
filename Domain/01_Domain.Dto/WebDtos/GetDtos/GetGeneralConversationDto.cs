namespace Domain.Dto.WebDtos.GetDtos
{
    public record GetGeneralConversationDto
    {
        public Guid Id { get; init; }
        public string GlobalContext { get; set; }
    }
}
