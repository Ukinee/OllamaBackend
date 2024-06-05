namespace Domain.Dto.WebDtos.GetDtos
{
    public class GetConcreteConversationDto
    {
        public Guid Id { get; init; }
        public string GlobalContext { get; set; }
        
        public List<GetMessageDto> Messages { get; init; }
    }
}
