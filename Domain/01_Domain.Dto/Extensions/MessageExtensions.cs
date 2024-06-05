using Domain.Dto.DataBaseDtos;
using Domain.Dto.WebDtos.GetDtos;
using Domain.Dto.WebDtos.PostDtos;

namespace Domain.Dto.Extensions
{
    public static class MessageExtensions
    {
        public static DatabaseMessageDto ToDatabaseMessage(this PostMessageDto message) =>
            new DatabaseMessageDto
            {
                Id = Guid.NewGuid(),
                ConversationDtoId = message.ConversationId,
                Content = message.Content,
                ChatName = message.AgentName,
                Role = message.Role,
                Images = message.Images,
                Timestamp = DateTime.UtcNow,
            };

        public static GetMessageDto ToGetMessageDto(this DatabaseMessageDto message)
        {
            return new GetMessageDto
            {
                Id = message.Id,
                Content = message.Content,
                Images = message.Images,
                ChatRole = message.Role,
                ChatName = message.ChatName,
                Timestamp = message.Timestamp,
            };
        }
    }
}
