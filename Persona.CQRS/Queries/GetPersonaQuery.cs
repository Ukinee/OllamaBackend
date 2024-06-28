using Common.DataAccess;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetPersonaQuery
    {
        private readonly IPersonaRepository _personaRepository;

        public GetPersonaQuery(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonaEntity> Execute(Guid id)
        {
            return await _personaRepository.Get(id) 
                   ?? throw new NotFoundException(nameof(PersonaEntity));
        }
    }
}
