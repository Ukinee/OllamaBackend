using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Services.Factories;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries;

public class CreatePersonaQuery
{
    private readonly IPersonaRepository _personaRepository;
    private readonly PersonaFactory _personaFactory;

    public CreatePersonaQuery(IPersonaRepository personaRepository, PersonaFactory personaFactory)
    {
        _personaRepository = personaRepository;
        _personaFactory = personaFactory;
    }
    
    public async Task<PersonaEntity> Execute(IdentityEntity identity, UserEntity user, CancellationToken token)
    {
        if(user.UserName == null)
        {
            throw new ArgumentNullException(nameof(user.UserName));
        }

        PersonaEntity entity = _personaFactory.Create(user.Id, identity, user.UserName);

        await _personaRepository.Add(entity, token);
        
        return entity;
    }
}

