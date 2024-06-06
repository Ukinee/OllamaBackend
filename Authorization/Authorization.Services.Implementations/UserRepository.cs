using Authorization.DataAccess;
using Authorization.Domain;
using Authorization.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Services.Implementations;

public class UserRepository(UserDbContext dbContext) : IUserRepository
{
    public async Task Add(UserEntity user)
    {
        await dbContext.Users.AddAsync(user);
        await Save();
    }

    public async Task<UserEntity> Update(UserRequest entity, string hashedPassword, string salt)
    {
        UserEntity userEntity = await Get(entity.Id);

        if(userEntity == null)
            throw new Exception("User not found during update.");
        
        userEntity.Name = entity.Name;
        userEntity.HashedPassword = hashedPassword;
        userEntity.Salt = salt;

        await Save();

        return userEntity;
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
        return dbContext.Users.AnyAsync(x => x.Name == name);
    }

    private async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }
}
