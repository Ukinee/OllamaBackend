namespace Domain.Dto.WebDtos.GetDtos
{
    public record GetMessageDto
    {
        public Guid Id { get; set; }
        public string ChatName { get; set; }
        public string ChatRole { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string[]? Images { get; set; }
    }
}
