namespace Chat.Domain.Messages.Base
{
    public record MessageBase
    {
        public required Guid PersonaId { get; init; }
        public required string Content { get; init; }
        public required bool IsSystem { get; init; }
        
        public bool IsRespond => RespondedMessageId.HasValue;
        public required Guid? RespondedMessageId { get; init; }
        
        public required string[] Images { get; init; }
    }
}
