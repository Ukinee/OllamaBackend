using Chat.Domain.Conversations.Base;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Core.Common.DataAccess.SharedEntities.Chats
{
    public record ConversationEntity : ConversationBase
    {
        public required Guid Id { get; init; }
        
        public required bool IsFinished { get; init; }
        public required DateTime CreatedAt { get; init; }
        public required DateTime EndedAt { get; init; }
        
        public required List<MessageEntity> Messages { get; init; }
        public required List<PersonaEntity> Personas { get; init; }
    }
}
