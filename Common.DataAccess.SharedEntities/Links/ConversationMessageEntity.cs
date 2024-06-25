namespace Common.DataAccess.SharedEntities.Links
{
    public class ConversationMessageEntity
    {
        public ConversationMessageEntity(Guid conversationId, Guid messageId)
        {
            ConversationId = conversationId;
            MessageId = messageId;
        }

        public Guid ConversationId { get; }
        public Guid MessageId { get; }
    }
}
