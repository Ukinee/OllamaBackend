using Domain.Dto.DataBaseDtos;
using Domain.Dto.WebDtos.GetDtos;
using Domain.Dto.WebDtos.PostDtos;

namespace Domain.Dto.Extensions
{
    public static class ConversationExtensions
    {
        public static DatabaseConversationDto ToDatabaseConversation(this PostConversationDto conversation)
        {
            return new DatabaseConversationDto
            {
                Id = Guid.NewGuid(),
                GlobalContext = conversation.GlobalContext,
                Messages = [],
            };
        }

        public static GetGeneralConversationDto ToGeneralConversation(this DatabaseConversationDto conversation)
        {
            return new GetGeneralConversationDto
            {
                Id = conversation.Id,
                GlobalContext = conversation.GlobalContext,
            };
        }

        public static GetConcreteConversationDto ToConcreteConversation(this DatabaseConversationDto conversation)
        {
            return new GetConcreteConversationDto
            {
                Id = conversation.Id,
                GlobalContext = conversation.GlobalContext,
                Messages = conversation.Messages.Select(x => x.ToGetMessageDto()).ToList(),
            };
        }
    }
}
