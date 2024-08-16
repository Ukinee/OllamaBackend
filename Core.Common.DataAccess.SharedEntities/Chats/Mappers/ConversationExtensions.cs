using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Core.Common.DataAccess.SharedEntities.Chats.Mappers
{
    public static class ConversationExtensions
    {
        public static GeneralConversationViewModel ToGeneralConversationViewModel(this ConversationEntity conversation)
        {
            return new GeneralConversationViewModel
            {
                Information = conversation.Information,
                Id = conversation.Id,
                Name = conversation.Name,
                Context = conversation.Context,
            };
        }

        public static ConcreteConversationViewModel ToConcreteConversation
        (
            this ConversationEntity conversation,
            IEnumerable<MessageEntity> messageEntities
        )
        {
            return new ConcreteConversationViewModel
            {
                Id = conversation.Id,
                Information = conversation.Information,
                Name = conversation.Name,
                PersonasId = conversation.Personas.Select(x => x.Id).ToList(),
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
                Information = conversation.Information,
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
