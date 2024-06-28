namespace Chat.Domain.Messages.Base
{
    public record MessageBase
    {
        public Guid PersonaId { get; set; }
        public string Content { get; set; }
        
        public string[]? Images { get; set; }
    }
}
