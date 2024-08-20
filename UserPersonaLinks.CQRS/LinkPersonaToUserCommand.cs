using Authorization.Services.Interfaces;
using Core.Common.DataAccess.SharedEntities.Users;

namespace UserPersonaLinks.CQRS;

public class LinkPersonaToUserCommand
{
    private readonly IUserRepository _userRepository;

    public LinkPersonaToUserCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Execute(UserEntity user, PersonaEntity persona, CancellationToken token)
    {
        user.Personas.Add(persona);

        await _userRepository.Save(token);
    }
}
