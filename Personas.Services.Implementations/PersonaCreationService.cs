using Common.DataAccess.SharedEntities.Users;
using Persona.Services.Factories;

namespace Personas.Services.Implementations
{
    public class PersonaCreationService
    {
        private readonly PersonaFactory _personaFactory;

        public PersonaCreationService(PersonaFactory personaFactory)
        {
            _personaFactory = personaFactory;
        }
        
        public PersonaEntity Create(Guid userId, IdentityEntity identity, string name)
        {
            
        }
    }
}
