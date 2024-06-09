using Microsoft.AspNetCore.Identity;

namespace Common.DataAccess.SharedEntities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; init; }
        
        public List<ConversationEntity> Conversations { get; init; } = [];
    }
}
