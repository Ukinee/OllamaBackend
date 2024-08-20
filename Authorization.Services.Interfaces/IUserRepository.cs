using Core.Common.DataAccess.SharedEntities.Users;

namespace Authorization.Services.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity?> Find(Func<UserEntity, bool> predicate);
        
        public Task Add(UserEntity user, CancellationToken token);
        
        public Task Save(CancellationToken token);
    }
}
