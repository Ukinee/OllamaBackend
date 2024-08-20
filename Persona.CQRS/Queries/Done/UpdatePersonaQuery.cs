using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries.Done
{
    public class UpdatePersonaQuery
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly PersonaMapper _personaMapper;

        public UpdatePersonaQuery(IPersonaRepository personaRepository, PersonaMapper personaMapper)
        {
            _personaRepository = personaRepository;
            _personaMapper = personaMapper;
        }

        public async Task<PersonaViewModel> Execute
        (
            PersonaEntity personaEntity,
            PutPersonaRequest putPersonaRequest,
            CancellationToken token
        )
        {
            personaEntity.Name = putPersonaRequest.Name;

            await _personaRepository.Save(token);

            return _personaMapper.ToViewModel(personaEntity);
        }
    }
}
