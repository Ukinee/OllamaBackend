using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class UpdatePersonaQuery
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly PersonaMapper _personaMapper;

        public UpdatePersonaQuery(IPersonaRepository personaRepository, PersonaMapper personaMapper)
        {
            _personaRepository = personaRepository;
            _personaMapper = personaMapper;
        }

        public async Task<PersonaViewModel> Execute(PutPersonaRequest updatePersonaRequest, Guid personaId)
        {
            await _personaRepository.Update(updatePersonaRequest, personaId);

            PersonaEntity? persona = await _personaRepository.Get(personaId);

            if (persona == null)
                throw new NotFoundException(nameof(persona));

            return _personaMapper.ToViewModel(persona);
        }
    }
}
