using Chat.Domain.Messages;

namespace Common.DataAccess.SharedEntities.Chats.Mappers
{
    public static class MessageExtensions
    {
        public static MessageEntity ToEntity(this PostMessageRequest message)
        {
            return new MessageEntity
            {
                Id = Guid.NewGuid(),
                SenderPersonaId = message.PersonaId,
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
                Images = message.Images,
                Timestamp = message.Timestamp,
            };
        }
    }
}
