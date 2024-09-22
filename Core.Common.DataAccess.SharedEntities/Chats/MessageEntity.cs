using System.ComponentModel.DataAnnotations.Schema;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Core.Common.DataAccess.SharedEntities.Chats
{
    public record MessageEntity
    {
        public required Guid Id { get; init; }
        public required string Content { get; init; }
        public required string[] Images { get; init; }
        public required DateTime Timestamp { get; init; }
        
        public required bool IsSystem { get; init; }
        
        public required Guid? RespondedMessageId { get; init; }
        
        [NotMapped]
        public bool IsRespond => RespondedMessageId.HasValue;
        
        //Connections
        public PersonaEntity? SenderPersona { get; init; }
        public required Guid SenderPersonaId { get; init; }
        
        public ConversationEntity? Conversation { get; init; }
        public required Guid ConversationId { get; init; }
    }
}
