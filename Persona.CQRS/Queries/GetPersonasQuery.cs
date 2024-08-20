using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Models.Personas;

namespace Persona.CQRS.Queries
{
    public class GetPersonasQuery
    {
        private readonly PersonaMapper _personaMapper;

        public GetPersonasQuery
        (
            PersonaMapper personaMapper
        )
        {
            _personaMapper = personaMapper;
        }

        public IList<PersonaViewModel> ExecuteByUser(UserEntity user)
        {
            return user
                .Personas
                .Select(x => _personaMapper.ToViewModel(x))
                .ToArray();
        }

        public IList<PersonaViewModel> ExecuteByConversation(ConversationEntity conversation)
        {
            return conversation
                .Personas
                .Select(x => _personaMapper.ToViewModel(x))
                .ToList();
        }
    }
}
