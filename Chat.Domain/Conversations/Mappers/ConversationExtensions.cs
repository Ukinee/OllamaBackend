using Chat.Domain.Messages.Mappers;

namespace Chat.Domain.Conversations.Mappers
{
    public static class ConversationExtensions
    {
        public static ConversationEntity ToEntity(this PostConversationRequest conversation)
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
