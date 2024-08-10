namespace Chat.Domain.Messages.Base
{
    public record MessageBase
    {
        public required Guid PersonaId { get; set; }
        public required string Content { get; set; }
        
        public  string[]? Images { get; set; }
    }
}
