using Common.DataAccess;
using Persona.Models;
using Persona.Models.Mappers;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetPersonaLinkQuery
    {
        private readonly IPersonaLinkRepository _personaLinkRepository;

        public GetPersonaLinkQuery(IPersonaLinkRepository personaLinkRepository)
        {
            _personaLinkRepository = personaLinkRepository;
        }

        public async Task<PersonaLinkViewModel> Execute(Guid conversationId, Guid userId)
        {
            PersonaLinkEntity? personaLinkEntity = await _personaLinkRepository.Get(conversationId, userId);

            if (personaLinkEntity == null)
            {
                throw new NotFoundException(nameof(PersonaLinkEntity));
            }

            return personaLinkEntity.ToViewModel();
        }
    }
}
