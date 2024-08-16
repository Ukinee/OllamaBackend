using Authorization.Domain.Base;

namespace Authorization.Domain
{
    public class UserCreateRequest : UserBase
    {
        public required string Password { get; set; }
    }
}
