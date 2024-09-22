namespace Memories.Domain
{
    public class PutMemoryRequest : MemoryBase
    {
        public required Guid Id { get; init; }
    }
}
