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

        public Task<UserEntity?> Find(Func<UserEntity, bool> predicate)
        {
            Expression<Func<UserEntity, bool>> expression = x => predicate.Invoke(x);
            
            return _dbContext
                .Users
                .Include(user => user.Personas)
                .FirstOrDefaultAsync(expression);
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
