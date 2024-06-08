using Domain.Dto.DataBaseDtos;
using Domain.Dto.WebDtos.GetDtos;
using Domain.Dto.WebDtos.PostDtos;

namespace Domain.Dto.Extensions
{
    public static class ConversationExtensions
    {
        public static ConversationEntity ToDatabaseConversation(this PostConversationRequest conversation)
        {
            return new ConversationEntity
            {
                Id = Guid.NewGuid(),
                Name = conversation.Name,
                GlobalContext = conversation.GlobalContext,
                Messages = [],
            };
        }

        public static GeneralConversationViewModel ToGeneralConversation(this ConversationEntity conversation)
        {
            return new GeneralConversationViewModel
            {
                Id = conversation.Id,
                Name = conversation.Name,
                GlobalContext = conversation.GlobalContext,
            };
        }

        public static ConcreteConversationViewModel ToConcreteConversation(this ConversationEntity conversation)
        {
            return new ConcreteConversationViewModel
            {
                Id = conversation.Id,
                Name = conversation.Name,
                GlobalContext = conversation.GlobalContext,
                Messages = conversation.Messages.Select(x => x.ToGetMessageDto()).ToList(),
            };
        }
    }
}
