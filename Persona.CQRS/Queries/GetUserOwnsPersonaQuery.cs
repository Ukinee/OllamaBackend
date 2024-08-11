using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetUserOwnsPersonaQuery
    {
        private readonly IPersonaRepository _personaRepository;

        public GetUserOwnsPersonaQuery(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<bool> Execute(Guid userId, Guid personaId)
        {
            PersonaEntity? persona = await _personaRepository.Get(personaId);

            if (persona == null)
                throw new NotFoundException(nameof(persona));
            
            return persona.UserId == userId;
        }
    }
}
