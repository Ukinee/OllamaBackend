namespace Domain.Models.Conversations.Base
{
    public record ConversationBase
    {
        public string Name { get; set; }
        public string GlobalContext { get; set; }
    }
}
