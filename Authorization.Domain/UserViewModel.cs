using Authorization.Domain.Base;

namespace Authorization.Domain
{
    public class UserViewModel : UserBase
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public List<Guid> ConversationIds { get; set; } = [];
    }
}
