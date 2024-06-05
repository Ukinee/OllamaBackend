namespace Domain.Dto.DataBaseDtos
{
    public record DatabaseConversationDto
    {
        public Guid Id { get; init; }
        public string GlobalContext { get; set; }
        
        public List<DatabaseMessageDto> Messages { get; init; } = new List<DatabaseMessageDto>();
    }
}
