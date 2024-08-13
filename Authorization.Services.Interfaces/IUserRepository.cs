using Authorization.Domain;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Users;

namespace Authorization.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity?> Get(Guid id);
        public Task<UserEntity?> Get(string username);
        
        public Task Add(UserEntity user);
        public Task Delete(UserEntity entity);

        public Task<bool> Exists(string name);
    }
}
