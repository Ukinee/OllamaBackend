using Chat.Domain.Messages;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Core.Common.DataAccess.SharedEntities.Chats.Mappers
{
    public static class MessageExtensions
    {
        public static MessageEntity ToEntity(this PostMessageRequest message, PersonaEntity personaEntity)
        {
            return new MessageEntity
            {
                Id = Guid.NewGuid(),
                SenderPersonaId = message.PersonaId,
                SenderPersona = personaEntity,
                ConversationId = message.ConversationId,
                Content = message.Content,
                Images = message.Images,
                Timestamp = DateTime.UtcNow,
            };
        }

        public static MessageViewModel ToViewModel(this MessageEntity message)
        {
            return new MessageViewModel
            {
                Id = message.Id,
                PersonaId = message.SenderPersonaId,
                Content = message.Content,
                SenderName = message.SenderPersona.Name,
                Images = message.Images,
                Timestamp = message.Timestamp,
            };
        }
    }
}
