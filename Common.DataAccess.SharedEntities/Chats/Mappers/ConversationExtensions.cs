using Chat.Domain.Conversations;
using Common.DataAccess.SharedEntities.Users;

namespace Common.DataAccess.SharedEntities.Chats.Mappers
{
    public static class ConversationExtensions
    {
        public static GeneralConversationViewModel ToGeneralConversation(this ConversationEntity conversation)
        {
            return new GeneralConversationViewModel
            {
                Id = conversation.Id,
                Name = conversation.Name,
                Context = conversation.Context,
            };
        }

        public static ConcreteConversationViewModel ToConcreteConversation
        (
            this ConversationEntity conversation,
            IList<MessageEntity> messageEntities
        )
        {
            return new ConcreteConversationViewModel
            {
                Id = conversation.Id,
                Name = conversation.Name,
                Context = conversation.Context,
                Messages = messageEntities.Select(x => x.ToViewModel()).ToList(),
            };
        }

        public static ConversationEntity ToEntity(this PostConversationRequest conversation, PersonaEntity ownerPersona)
        {
            return new ConversationEntity
            {
                Id = Guid.NewGuid(),
                IsFinished = false,
                CreatedAt = DateTime.Now,
                EndedAt = DateTime.MaxValue,
                Name = conversation.Name,
                Context = conversation.Context,
                Messages = [],
                Personas = [ownerPersona],
            };
        }
    }
}
