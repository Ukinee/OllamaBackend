using Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetPersonasQuery
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly PersonaMapper _personaMapper;

        public GetPersonasQuery(IPersonaRepository personaRepository, PersonaMapper personaMapper)
        {
            _personaRepository = personaRepository;
            _personaMapper = personaMapper;
        }

        public async Task<PersonasViewModel> Execute(Guid userId)
        {
            PersonaEntity[] personas = await _personaRepository.GetAll(userId);

            PersonasViewModel personasViewModel = new PersonasViewModel
            {
                Personas = personas.Select(x => _personaMapper.ToViewModel(x)).ToArray(),
            };
            
            Console.WriteLine(personasViewModel.Personas.Count);

            return personasViewModel;
        }
    }
}
