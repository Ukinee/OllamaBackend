using Chat.Domain.Conversations;
using Chat.Domain.Messages;

namespace Common.DataAccess.SharedEntities.Mappers
{
    public static class ConversationExtensions
    {
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
                Messages = conversation.Messages.Select<MessageEntity, MessageViewModel>(x => x.ToViewModel()).ToList(),
            };
        }
        
        public static ConversationEntity ToEntity(this PostConversationRequest conversation, Guid ownerId)
        {
            return new ConversationEntity
            {
                Id = Guid.NewGuid(),
                Name = conversation.Name,
                OwnerId = ownerId,
                GlobalContext = conversation.GlobalContext,
                Messages = [],
            };
        }
    }
}
