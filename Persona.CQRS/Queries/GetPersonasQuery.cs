using Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetPersonasQuery
    {
        private readonly IPersonasRepository _personasRepository;
        private readonly PersonaMapper _personaMapper;

        public GetPersonasQuery(IPersonasRepository personasRepository, PersonaMapper personaMapper)
        {
            _personasRepository = personasRepository;
            _personaMapper = personaMapper;
        }

        public async Task<PersonasViewModel> Execute(Guid userId)
        {
            PersonaEntity[] personas = await _personasRepository.GetAll(userId);

            PersonasViewModel personasViewModel = new PersonasViewModel
            {
                Personas = personas.Select(x => _personaMapper.ToViewModel(x)).ToArray(),
            };
            
            Console.WriteLine(personasViewModel.Personas.Count);

            return personasViewModel;
        }
    }
}
