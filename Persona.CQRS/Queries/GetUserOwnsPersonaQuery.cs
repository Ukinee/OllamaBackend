using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetUserOwnsPersonaQuery
    {
        private readonly IPersonasRepository _personasRepository;

        public GetUserOwnsPersonaQuery(IPersonasRepository personasRepository)
        {
            _personasRepository = personasRepository;
        }

        public async Task<bool> Execute(Guid userId, Guid personaId)
        {
            PersonaEntity? persona = await _personasRepository.Get(personaId);

            if (persona == null)
                throw new NotFoundException(nameof(persona));
            
            return persona.UserId == userId;
        }
    }
}
