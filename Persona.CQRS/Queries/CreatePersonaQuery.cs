using Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries;

public class CreatePersonaQuery
{
    private readonly PersonaMapper _personaMapper;

    public CreatePersonaQuery(PersonaMapper personaMapper)
    {
        _personaMapper = personaMapper;
    }
    
    public async Task<PersonaViewModel> Execute(PostPersonaRequest createPersonaRequest, Guid userId)
    {
        PersonaEntity entity = await _personaMapper.CreateEntity(createPersonaRequest, userId);
        
        return _personaMapper.ToViewModel(entity);
    }
}
