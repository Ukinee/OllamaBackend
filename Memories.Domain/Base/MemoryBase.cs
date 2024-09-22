namespace Memories.Domain
{
    public class MemoryBase
    {
        public required string Topic { get; init; }
        public required string Content { get; init; }
        
        public required bool IsConfirmed { get; init; }
        
        public required Guid PersonaId { get; init; }
    }
}
