using Common.DataAccess.SharedEntities.Chats;

namespace Common.DataAccess.SharedEntities.Users
{
    public class PersonaEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        
        public bool IsDeleted { get; set; }

        //Connections
        public required Guid UserId { get; set; }
        public UserEntity? User { get; set; }

        public required Guid IdentityId { get; set; }
        public IdentityEntity? Identity { get; set; }
        
        public required List<ConversationEntity> Conversations { get; set; }
    }
}
