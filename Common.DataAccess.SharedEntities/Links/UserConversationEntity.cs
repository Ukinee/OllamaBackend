namespace Common.DataAccess.SharedEntities.Links
{
    public class UserConversationEntity
    {
        public UserConversationEntity(Guid userId, Guid conversationId)
        {
            UserId = userId;
            ConversationId = conversationId;
        }

        public Guid UserId { get; }
        public Guid ConversationId { get; }
    }
}
