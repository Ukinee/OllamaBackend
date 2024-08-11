using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
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
            PersonaEntity? personaEntity = await _personaRepository.Get(id);
            
            if (personaEntity == null)
                throw new NotFoundException(nameof(PersonaEntity));

            return personaEntity;
        }
    }
}
