using Authorization.Domain;
using Common.DataAccess.SharedEntities;

namespace Authorization.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task Add(UserEntity user);
        public Task<UserEntity?> Get(Guid id);
        public Task Delete(UserEntity entity);

        public Task<bool> Exists(string name);
    }
}
