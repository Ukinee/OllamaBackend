namespace Chat.Domain.Conversations.Base
{
    public record ConversationBase
    {
        public required string Name { get; init; }
        public required string Information { get; init; }
        public required string Context { get; init; }
    }
}
