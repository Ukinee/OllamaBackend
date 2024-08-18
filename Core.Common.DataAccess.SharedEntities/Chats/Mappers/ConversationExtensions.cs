using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Core.Common.DataAccess.SharedEntities.Chats.Mappers
{
    public static class ConversationExtensions
    {
        public static ConversationViewModel ToViewModel(this ConversationEntity conversation)
        {
            return new ConversationViewModel
            {
                Id = conversation.Id,
                Information = conversation.Information,
                Name = conversation.Name,
                Context = conversation.Context,
                PersonasId = conversation.Personas.Select(x => x.Id).ToList(),
                Messages = conversation.Messages.Select(x => x.ToViewModel()).ToList(),
            };
        }

        public static ConversationViewModel ToViewModel(this ConversationEntity conversation, IList<MessageEntity> messages)
        {
            return new ConversationViewModel
            {
                Id = conversation.Id,
                Information = conversation.Information,
                Name = conversation.Name,
                Context = conversation.Context,
                PersonasId = conversation.Personas.Select(x => x.Id).ToList(),
                Messages = messages.Select(x => x.ToViewModel()).ToList(),
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
