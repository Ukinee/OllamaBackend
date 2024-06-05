namespace Domain.Dto.WebDtos.PostDtos
{
    public class PostMessageDto
    {
        public Guid ConversationId { get; set; }
        
        public string AgentName { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }
        public string[]? Images { get; set; }
    }
}
