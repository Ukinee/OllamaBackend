using Authorization.DataAccess;
using Authorization.Domain;
using Authorization.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Services.Implementations
{
    public class UserRepository(UserDbContext dbContext) : IUserRepository
    {
        public async Task Add(UserEntity user)
        {
            await dbContext.Users.AddAsync(user);
            await Save();
        }

        public Task<UserEntity?> Get(Guid id)
        {
            return dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(UserEntity entity)
        {
            dbContext.Users.Remove(entity);
            await Save();
        }

        public Task<bool> Exists(string name)
        {
            return dbContext.Users.AnyAsync(x => x.UserName == name);
        }

        private async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
