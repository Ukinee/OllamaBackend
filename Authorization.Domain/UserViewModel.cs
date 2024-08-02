using Authorization.Domain.Base;

namespace Authorization.Domain
{
    public class UserViewModel : UserBase
    {
        public required Guid Id { get; set; }
        public required string Token { get; set; }
        public required List<Guid> PersonasIds { get; set; }
    }
}
