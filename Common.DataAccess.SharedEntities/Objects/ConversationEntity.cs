using Chat.Domain.Conversations.Base;

namespace Common.DataAccess.SharedEntities.Objects
{
    public record ConversationEntity : ConversationBase
    {
        public Guid Id { get; init; }
    }
}
