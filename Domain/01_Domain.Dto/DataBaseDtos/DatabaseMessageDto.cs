using Domain.Dto.WebDtos.GetDtos;

namespace Domain.Dto.DataBaseDtos
{
    public record DatabaseMessageDto
    {
        public Guid Id { get; set; }
        
        public Guid? ConversationDtoId { get; set; }
        public DatabaseConversationDto? ConversationDto { get; set; }

        public string ChatName { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string[]? Images { get; set; }
    }
}
