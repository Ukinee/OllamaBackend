using Common.DataAccess.SharedEntities.Users;
using Persona.DomainServices;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries;

public class CreatePersonaQuery
{
    private readonly IPersonaRepository _personaRepository;
    private readonly PersonaMapper _personaMapper;

    public CreatePersonaQuery(IPersonaRepository personaRepository, PersonaMapper personaMapper)
    {
        _personaRepository = personaRepository;
        _personaMapper = personaMapper;
    }
    
    public async Task<PersonaViewModel> Execute(PostPersonaRequest createPersonaRequest, Guid userId)
    {
        PersonaEntity entity = _personaMapper.ToEntity(createPersonaRequest, userId);
        
        await _personaRepository.Add(entity);
        
        return _personaMapper.ToViewModel(entity);
    }
}
