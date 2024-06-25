namespace Common.DataAccess.SharedEntities.Links
{
    public class UserMessageEntity
    {
        public UserMessageEntity(Guid userId, Guid messageId)
        {
            UserId = userId;
            MessageId = messageId;
        }

        public Guid UserId { get; }
        public Guid MessageId { get; }
    }
}
