using Authorization.Domain;
using Authorization.Services.Factories;
using Common.Extensions;
using Core.Common.DataAccess.SharedEntities.Users;
using Microsoft.AspNetCore.Identity;

namespace Users.CQRS
{
    public class CreateUserQuery
    {
        private readonly UserFactory _userFactory;
        private readonly UserManager<UserEntity> _userManager;

        public CreateUserQuery
        (
            UserFactory userFactory,
            UserManager<UserEntity> userManager
        )
        {
            _userFactory = userFactory;
            _userManager = userManager;
        }

        public async Task<UserEntity> Create(UserCreateRequest createRequest)
        {
            UserEntity user = await CreateUser(createRequest);
                
            await AssignRoles(user);

            return user;
        }

        private async Task<UserEntity> CreateUser(UserCreateRequest createRequest)
        {
            UserEntity user = _userFactory.Create(createRequest, "User"); //todo: hardcode
            IdentityResult result = await _userManager.CreateAsync(user, createRequest.Password);

            if (result.Succeeded == false)
                throw new Exception(result.GetErrors());

            return user;
        }

        private async Task AssignRoles(UserEntity user)
        {
            IdentityResult result = await _userManager.AddToRoleAsync(user, "User"); //todo : hardcode

            if (result.Succeeded == false)
                throw new Exception(result.GetErrors());
        }
    }
}
