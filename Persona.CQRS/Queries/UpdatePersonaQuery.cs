using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class UpdatePersonaQuery
    {
        private readonly IPersonasRepository _personasRepository;
        private readonly PersonaMapper _personaMapper;

        public UpdatePersonaQuery(IPersonasRepository personasRepository, PersonaMapper personaMapper)
        {
            _personasRepository = personasRepository;
            _personaMapper = personaMapper;
        }

        public async Task<PersonaViewModel> Execute(PutPersonaRequest updatePersonaRequest, Guid personaId)
        {
            await _personasRepository.Update(updatePersonaRequest, personaId);

            PersonaEntity? persona = await _personasRepository.Get(personaId);

            if (persona == null)
                throw new NotFoundException(nameof(persona));

            return _personaMapper.ToViewModel(persona);
        }
    }
}
