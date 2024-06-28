using Chat.Domain.Messages;

namespace Common.DataAccess.SharedEntities.Mappers
{
    public static class MessageExtensions
    {
        public static MessageEntity ToEntity(this PostMessageRequest message, Guid userId)
        {
            return new MessageEntity
            {
                Id = Guid.NewGuid(),
                SenderId = userId,
                PersonaId = message.PersonaId,
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
                SenderId = message.SenderId,
                Content = message.Content,
                PersonaId = message.PersonaId,
                Images = message.Images,
                Timestamp = message.Timestamp,
            };
        }
    }
}
