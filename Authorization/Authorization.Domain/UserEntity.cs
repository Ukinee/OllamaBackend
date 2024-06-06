using Authorization.Domain.Base;
using Domain.Dto.DataBaseDtos;

namespace Authorization.Domain;

public class UserEntity : UserBase
{
    public Guid Id { get; set; }
    
    public string HashedPassword { get; set; }
    public string Salt { get; set; }

    public List<Guid> ConversationIds { get; set; } = [];
    
    public DateTime CreatedAt { get; set; }
}
