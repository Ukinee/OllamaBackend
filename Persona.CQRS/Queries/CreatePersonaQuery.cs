using Persona.Models.Mappers;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries;

public class CreatePersonaQuery
{
    private readonly IPersonaRepository _personaRepository;

    public CreatePersonaQuery(IPersonaRepository personaRepository)
    {
        _personaRepository = personaRepository;
    }
    
    public async Task<PersonaViewModel> Execute(PostPersonaRequest createPersonaRequest)
    {
        PersonaEntity entity = createPersonaRequest.ToEntity();
        
        await _personaRepository.Add(entity);
        
        return entity.ToViewModel();
    }
}
