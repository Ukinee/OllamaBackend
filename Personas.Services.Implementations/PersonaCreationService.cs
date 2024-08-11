using Common.DataAccess.SharedEntities.Users;
using Persona.Services.Factories;
using Personas.Services.Interfaces;

namespace Personas.Services.Implementations
{
    public class PersonaCreationService : IPersonaCreationService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly PersonaFactory _personaFactory;

        public PersonaCreationService(IPersonaRepository personaRepository, PersonaFactory personaFactory)
        {
            _personaRepository = personaRepository;
            _personaFactory = personaFactory;
        }
        
        public async Task<PersonaEntity> Create(Guid userId, IdentityEntity identity, string name)
        {
            PersonaEntity personaEntity = _personaFactory.Create(userId, identity, name);
            await _personaRepository.Add(personaEntity);

            return personaEntity;
        }
    }
}
