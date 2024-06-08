using Microsoft.AspNetCore.Identity;

namespace Authorization.Domain
{
    public class UserEntity : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; init; }
        
        public List<Guid> ConversationIds { get; init; } = [];
        public List<Guid> MessagesIds { get; init; } = [];
    }
}
