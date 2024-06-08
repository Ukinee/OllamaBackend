using Microsoft.AspNetCore.Identity;

namespace Authorization.Domain
{
    public class UserEntity : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public List<Guid> ConversationIds { get; set; } = new List<Guid>();
    }
}
