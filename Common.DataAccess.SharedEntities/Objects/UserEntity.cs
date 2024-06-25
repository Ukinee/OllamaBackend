using Microsoft.AspNetCore.Identity;

namespace Common.DataAccess.SharedEntities.Objects
{
    public class UserEntity : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; init; }
    }
}
