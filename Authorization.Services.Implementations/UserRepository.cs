using Authorization.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Services.Implementations
{
    public class UserRepository : IUserRepository
    {
        private UserContext _dbContext;

        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
            await Save();
        }

        public Task<UserEntity?> Get(Guid id)
        {
            return _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(UserEntity entity)
        {
            _dbContext.Users.Remove(entity);
            await Save();
        }

        public Task<bool> Exists(string name)
        {
            return _dbContext.Users.AnyAsync(x => x.UserName == name);
        }

        private async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
