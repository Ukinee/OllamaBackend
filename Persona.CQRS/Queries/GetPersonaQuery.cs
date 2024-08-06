using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetPersonaQuery
    {
        private readonly IPersonasRepository _personasRepository;

        public GetPersonaQuery(IPersonasRepository personasRepository)
        {
            _personasRepository = personasRepository;
        }

        public async Task<PersonaEntity> Execute(Guid id)
        {
            PersonaEntity? personaEntity = await _personasRepository.Get(id);
            
            if (personaEntity == null)
                throw new NotFoundException(nameof(PersonaEntity));

            return personaEntity;
        }
    }
}
