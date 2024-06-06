using Authorization.Domain;

namespace Authorization.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task Add(UserEntity user);
        public Task<UserEntity> Update(UserRequest entity, string hashedPassword, string salt);
        public Task<UserEntity?> Get(Guid id);
        public Task Delete(UserEntity entity);
        
        public Task<bool> Exists(string name);
    }
}
