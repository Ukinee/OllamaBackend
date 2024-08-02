using Authorization.Domain;
using Authorization.Services.Factories;
using Authorization.Services.Interfaces;
using Common.DataAccess.SharedEntities.Users;
using Identities.Services.Interfaces;
using Persona.Services.Factories;

namespace Authorization.Services.Implementations
{
    public class UserCreationService : IUserCreationService
    {
        private readonly PersonaFactory _personaFactory;
        private readonly IIdentityCreationService _identityCreationService;
        private readonly UserFactory _userFactory;

        public UserCreationService
        (
            PersonaFactory personaFactory,
            IIdentityCreationService identityCreationService,
            UserFactory userFactory
        )
        {
            _personaFactory = personaFactory;
            _identityCreationService = identityCreationService;
            _userFactory = userFactory;
        }

        public UserEntity Create(UserCreateRequest createRequest)
        {
            Guid userId = Guid.NewGuid();

            IdentityEntity identity = _identityCreationService.Create();
            PersonaEntity persona = _personaFactory.Create(userId, identity, createRequest.UserName);
            UserEntity user = _userFactory.Create(userId, createRequest, persona, "User"); //todo : hardcode

            return user;
        }
    }
}
