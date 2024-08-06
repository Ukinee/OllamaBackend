using Common.DataAccess.SharedEntities.Users;
using Persona.Services.Factories;
using Personas.Services.Interfaces;

namespace Personas.Services.Implementations
{
    public class PersonaCreationService : IPersonaCreationService
    {
        private readonly IPersonasRepository _personasRepository;
        private readonly PersonaFactory _personaFactory;

        public PersonaCreationService(IPersonasRepository personasRepository, PersonaFactory personaFactory)
        {
            _personasRepository = personasRepository;
            _personaFactory = personaFactory;
        }
        
        public async Task<PersonaEntity> Create(Guid userId, IdentityEntity identity, string name)
        {
            PersonaEntity personaEntity = _personaFactory.Create(userId, identity, name);
            await _personasRepository.Add(personaEntity);

            return personaEntity;
        }
    }
}
