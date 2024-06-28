using Persona.Models.Mappers;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetPersonasQuery
    {
        private readonly IPersonaRepository _personaRepository;

        public GetPersonasQuery(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonasViewModel> Execute(Guid userId)
        {
            PersonaEntity[] personas = await _personaRepository.GetAll(userId);
            
            PersonasViewModel personasViewModel = new PersonasViewModel
            {
                Personas = personas.Select(x => x.ToViewModel()).ToArray(),
            };
            
            return personasViewModel;
        }
    }
}
