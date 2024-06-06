using Authorization.Domain.Base;

namespace Authorization.Domain
{
    public class UserRequest : UserBase
    {
        public Guid Id { get; set; }
        
        public string Password { get; set; }
    }
}
