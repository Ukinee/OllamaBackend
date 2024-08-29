using Authorization.Domain.Base;

namespace Authorization.Domain
{
    public class UserLoginRequest : UserBase
    {
        public required string Password { get; set; }
    }
}
