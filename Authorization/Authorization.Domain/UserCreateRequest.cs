using Authorization.Domain.Base;

namespace Authorization.Domain
{
    public class UserCreateRequest : UserBase
    {
        public string Password { get; set; }
    }
}
