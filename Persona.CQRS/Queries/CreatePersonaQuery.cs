using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;

namespace Persona.CQRS.Queries;

public class CreatePersonaQuery
{
    private readonly PersonaMapper _personaMapper;

    public CreatePersonaQuery(PersonaMapper personaMapper)
    {
        _personaMapper = personaMapper;
    }
    
    public async Task<PersonaViewModel> Execute(PostPersonaRequest createPersonaRequest)
    {
        PersonaEntity entity = await _personaMapper.CreateEntity(createPersonaRequest);
        
        return _personaMapper.ToViewModel(entity);
    }
}
