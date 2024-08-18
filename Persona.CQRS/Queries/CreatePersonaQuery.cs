using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Services.Factories;

namespace Persona.CQRS.Queries;

public class CreatePersonaQuery
{
    private readonly PersonaFactory _personaFactory;

    public CreatePersonaQuery(PersonaFactory personaFactory)
    {
        _personaFactory = personaFactory;
    }
    
    public PersonaEntity Execute(IdentityEntity identity, UserEntity user)
    {
        if(user.UserName == null)
        {
            throw new ArgumentNullException(nameof(user.UserName));
        }
        
        return _personaFactory.Create(user.Id, identity, user.UserName);
    }
}

