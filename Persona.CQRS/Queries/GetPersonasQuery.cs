using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Mapster;
using Persona.Models.Personas;

namespace Persona.CQRS.Queries
{
    public class GetPersonasQuery
    {
        public IList<PersonaViewModel> ExecuteByUser(UserEntity user)
        {
            return user
                .Personas
                .Select(x => x.Adapt<PersonaViewModel>())
                .ToArray();
        }

        public IList<PersonaViewModel> ExecuteByConversation(ConversationEntity conversation)
        {
            return conversation
                .Personas
                .Select(x => x.Adapt<PersonaViewModel>())
                .ToList();
        }
    }
}
