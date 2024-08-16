using Authorization.Domain;
using Authorization.Services.Factories;
using Authorization.Services.Interfaces;
using Core.Common.DataAccess.SharedEntities.Users;
using Identities.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Personas.Services.Interfaces;

namespace Authorization.Services.Implementations
{
    public class UserCreationService : IUserCreationService
    {
        private readonly IPersonaCreationService _personaCreationService;
        private readonly IIdentityCreationService _identityCreationService;
        private readonly UserFactory _userFactory;
        private readonly UserManager<UserEntity> _userManager;

        public UserCreationService
        (
            IPersonaCreationService personaCreationService,
            IIdentityCreationService identityCreationService,
            UserFactory userFactory,
            UserManager<UserEntity> userManager
        )
        {
            _personaCreationService = personaCreationService;
            _identityCreationService = identityCreationService;
            _userFactory = userFactory;
            _userManager = userManager;
        }

        public async Task<UserEntity> Create(UserCreateRequest createRequest)
        {
            UserEntity user = await CreateUser(createRequest);
                
            await AssignRoles(user);

            IdentityEntity identity = await _identityCreationService.Create();
            PersonaEntity persona = await _personaCreationService.Create(user.Id, identity, createRequest.UserName);
            user.Personas.Add(persona);
            
            return user;
        }

        private async Task<UserEntity> CreateUser(UserCreateRequest createRequest)
        {
            UserEntity user = _userFactory.Create(createRequest, "User"); 
            IdentityResult result = await _userManager.CreateAsync(user, createRequest.Password);

            if (result.Succeeded == false)
                throw new Exception(result.Errors.Select(x => x.Description).Aggregate((x, y) => x + ", " + y));

            return user;
        }

        private async Task AssignRoles(UserEntity user)
        {
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User"); //todo : hardcode

            if (roleResult.Succeeded == false)
                throw new Exception(roleResult.Errors.Select(x => x.Description).Aggregate((x, y) => x + ", " + y));
        }
    }
}
