using Authorization.Domain;
using Core.Common.DataAccess.SharedEntities.Users;
using Identities.SQRS;
using Persona.CQRS.Queries;
using Users.CQRS;

namespace Authorization.Services.Implementations
{
    public class UserService
    {
        private readonly CreateUserQuery _createUserQuery;
        private readonly CreatePersonaQuery _createPersonaQuery;
        private readonly CreateIdentityQuery _createIdentityQuery;

        public UserService
        (
            CreateUserQuery createUserQuery,
            CreatePersonaQuery createPersonaQuery,
            CreateIdentityQuery createIdentityQuery
        )
        {
            _createUserQuery = createUserQuery;
            _createPersonaQuery = createPersonaQuery;
            _createIdentityQuery = createIdentityQuery;
        }

        public async Task<UserViewModel> Create(UserCreateRequest userCreateRequest)
        {
            UserEntity userEntity = await _createUserQuery.Create(userCreateRequest);
            IdentityEntity identityEntity = _createIdentityQuery.Execute();
            PersonaEntity persona = _createPersonaQuery.Execute(identityEntity, userEntity);
            
            throw new NotImplementedException("Link persona to user");

            return userEntity.ToViewModel();
        }
    }
}
