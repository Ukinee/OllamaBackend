using System.ComponentModel.DataAnnotations;
using Authorization.Domain.Base;

namespace Authorization.Domain
{
    public class UserCreateRequest : UserBase
    {
        [Required] public string Password { get; set; }
    }
}
