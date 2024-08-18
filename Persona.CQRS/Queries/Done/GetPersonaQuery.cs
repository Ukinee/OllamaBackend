using Core.Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries.Done
{
    public class GetPersonaQuery
    {
        private readonly IPersonaRepository _personaRepository;
        
        public GetPersonaQuery(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonaEntity> Execute(Guid guid, CancellationToken token)
        {
            PersonaEntity? persona = await _personaRepository.Find(x => x.Id == guid, token);
            
            if(persona is null)
                throw new InvalidOperationException($"Persona with Id {guid} not found"); //todo: hardcode

            if (persona.IsDeleted)
                throw new InvalidOperationException($"Persona with Id {guid} deleted and cannot be accessed"); //todo: hardcode

            return persona;
        }
    }
}
