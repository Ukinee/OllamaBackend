using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities.Objects;

namespace Common.DataAccess.SharedEntities.Mappers
{
    public static class ConversationExtensions
    {
        public static ConversationEntity ToEntity(this PostConversationRequest conversation)
        {
            return new ConversationEntity
            {
                Id = Guid.NewGuid(),
                Name = conversation.Name,
                GlobalContext = conversation.GlobalContext
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

        public static ConcreteConversationViewModel ToConcreteConversation
        (
            this ConversationEntity conversation,
            IEnumerable<MessageEntity> messages
        )
        {
            return new ConcreteConversationViewModel
            {
                Id = conversation.Id,
                Name = conversation.Name,
                GlobalContext = conversation.GlobalContext,
                Messages = messages.Select(x => x.ToViewModel()).ToList()
            };
        }
    }
}
