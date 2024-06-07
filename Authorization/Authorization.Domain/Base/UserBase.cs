using System.ComponentModel.DataAnnotations;

namespace Authorization.Domain.Base
{
    public class UserBase
    {
        [Required]
        public string UserName { get; set; }
    }
}
