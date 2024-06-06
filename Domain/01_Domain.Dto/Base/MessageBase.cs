namespace Domain.Dto.Base
{
    public record MessageBase
    {
        public string ChatName { get; set; }
        public string ChatRole { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string[]? Images { get; set; }
    }
}
