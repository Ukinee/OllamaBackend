using Chat.Domain.Conversations.Base;
using Common.DataAccess.SharedEntities.Users;

namespace Common.DataAccess.SharedEntities.Chats
{
    public record ConversationEntity : ConversationBase
    {
        public required Guid Id { get; init; }
        
        public required bool IsFinished { get; init; }
        public required DateTime CreatedAt { get; init; }
        public required DateTime EndedAt { get; init; }
        
        public required ICollection<MessageEntity> Messages { get; init; }
        public required ICollection<PersonaEntity> Personas { get; init; }
    }
}
