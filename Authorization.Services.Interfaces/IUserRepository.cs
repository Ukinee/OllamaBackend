using Authorization.Domain;

namespace Authorization.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task Add(UserEntity user);
        public Task<UserEntity?> Get(Guid id);
        public Task Delete(UserEntity entity);

        public Task<bool> Exists(string name);
        
        public Task AddConversationToUser(UserEntity userEntity, Guid conversationId);
    }
}
