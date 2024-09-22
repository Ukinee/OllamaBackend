using System.Linq.Expressions;
using Authorization.Services.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Users;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Services.Implementations
{
    public class UserRepository : IUserRepository
    {
        private CompositeContext _dbContext;

        public UserRepository(CompositeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserEntity?> Find(Expression<Func<UserEntity, bool>> predicate)
        {
            return _dbContext
                .Users
                .Include(user => user.Personas)
                .ThenInclude(x => x.Conversations) //todo: bad
                .FirstOrDefaultAsync(predicate);
        }

        public async Task Add(UserEntity user, CancellationToken token)
        {
            await _dbContext.Users.AddAsync(user, token);
            
            await Save(token);
        }

        public Task<bool> Exists(string name)
        {
            return _dbContext.Users.AnyAsync(x => x.UserName == name);
        }

        public async Task Save(CancellationToken token)
        {
            await _dbContext.SaveChangesAsync(token);
        }
    }
}
