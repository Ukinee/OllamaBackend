using Persona.Models;
using Persona.Models.Mappers;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Commands
{
    public class UpdatePersonaAssociationCommand
    {
        private readonly IPersonaLinkRepository _personaLinkRepository;

        public UpdatePersonaAssociationCommand(IPersonaLinkRepository personaLinkRepository)
        {
            _personaLinkRepository = personaLinkRepository;
        }
        
        public async Task Execute(PutPersonaLinkRequest request, Guid userId)
        {
            PersonaLinkEntity entity = request.ToEntity(userId);
            
            await _personaLinkRepository.Update(entity);
        }
    }
}
