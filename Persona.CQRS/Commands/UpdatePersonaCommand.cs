using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries.Done
{
    public class UpdatePersonaCommand
    {
        private readonly IPersonaRepository _personaRepository;

        public UpdatePersonaCommand(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task Execute
        (
            PersonaEntity personaEntity,
            PutPersonaRequest putPersonaRequest,
            CancellationToken token
        )
        {
            personaEntity.Name = putPersonaRequest.Name;

            await _personaRepository.Save(token);
        }
    }
}
