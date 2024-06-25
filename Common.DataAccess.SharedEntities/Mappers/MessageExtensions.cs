using Chat.Domain.Messages;
using Common.DataAccess.SharedEntities.Objects;

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
                Content = message.Content,
                ChatName = message.ChatName,
                ChatRole = message.ChatRole,
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
                Images = message.Images,
                ChatRole = message.ChatRole,
                ChatName = message.ChatName,
                Timestamp = message.Timestamp,
            };
        }
    }
}
