namespace Chat.Domain.Conversations.Base
{
    public class ConversationBase
    {
        public required string Name { get; set; }
        public required string Information { get; set; }
        public required string Context { get; set; }
    }
}
