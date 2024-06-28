using Common.DataAccess;
using Persona.Models.Mappers;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class UpdatePersonaQuery
    {
        private readonly IPersonaRepository _personaRepository;

        public UpdatePersonaQuery(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonaViewModel> Execute(PutPersonaRequest updatePersonaRequest)
        {
            await _personaRepository.Update(updatePersonaRequest);

            PersonaEntity? persona = await _personaRepository.Get(updatePersonaRequest.Id);

            if (persona == null)
                throw new NotFoundException(nameof(persona));

            return persona.ToViewModel();
        }
    }
}
