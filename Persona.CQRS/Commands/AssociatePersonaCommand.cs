using Persona.Models;
using Persona.Models.Mappers;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Commands
{
    public class AssociatePersonaCommand
    {
        private readonly IPersonaLinkRepository _personaLinkRepository;

        public AssociatePersonaCommand(IPersonaLinkRepository personaLinkRepository)
        {
            _personaLinkRepository = personaLinkRepository;
        }

        public async Task Execute(PostPersonaLinkRequest request, Guid userId)
        {
            PersonaLinkEntity entity = request.ToEntity(userId);

            await _personaLinkRepository.Add(entity);
        }
    }
}
