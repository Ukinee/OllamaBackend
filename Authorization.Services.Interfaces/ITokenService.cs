using Core.Common.DataAccess.SharedEntities.Users;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(UserManager<UserEntity> userManager, UserEntity user);
    }
}
