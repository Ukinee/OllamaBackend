using Authorization.Domain;
using Common.DataAccess.SharedEntities;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(UserManager<UserEntity> userManager, UserEntity user);
    }
}
