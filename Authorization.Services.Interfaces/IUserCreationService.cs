using Authorization.Domain;
using Common.DataAccess.SharedEntities.Users;

namespace Authorization.Services.Interfaces
{
    public interface IUserCreationService
    {
        public Task<UserEntity> Create(UserCreateRequest createRequest);
    }
}
