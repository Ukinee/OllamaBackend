using Authorization.Domain;
using Core.Common.DataAccess.SharedEntities.Users;
using Identities.SQRS;
using Persona.CQRS.Queries;
using UserPersonaLinks.CQRS;
using Users.CQRS;

namespace Authorization.Services.Implementations
{
    public class UserService
    {
        private readonly CreateUserQuery _createUserQuery;
        private readonly CreatePersonaQuery _createPersonaQuery;
        private readonly CreateIdentityQuery _createIdentityQuery;
        private readonly LinkPersonaToUserCommand _linkPersonaToUserCommand;

        public UserService
        (
            CreateUserQuery createUserQuery,
            CreatePersonaQuery createPersonaQuery,
            CreateIdentityQuery createIdentityQuery,
            LinkPersonaToUserCommand linkPersonaToUserCommand
        )
        {
            _createUserQuery = createUserQuery;
            _createPersonaQuery = createPersonaQuery;
            _createIdentityQuery = createIdentityQuery;
            _linkPersonaToUserCommand = linkPersonaToUserCommand;
        }

        public async Task<UserViewModel> Create(UserCreateRequest userCreateRequest, CancellationToken token)
        {
            UserEntity user = await _createUserQuery.Create(userCreateRequest);
            IdentityEntity identity = await _createIdentityQuery.Execute(token);
            PersonaEntity persona = await _createPersonaQuery.Execute(identity, user, token);
            
            await _linkPersonaToUserCommand.Execute(user, persona, token);

            return user.ToViewModel();
        }
    }
}
