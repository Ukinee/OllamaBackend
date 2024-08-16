using Authorization.Domain;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Authorization.Services.Interfaces
{
    public interface IUserCreationService
    {
        public Task<UserEntity> Create(UserCreateRequest createRequest);
    }
}
