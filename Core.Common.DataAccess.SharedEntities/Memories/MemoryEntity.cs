using Core.Common.DataAccess.SharedEntities.Users;
using Memories.Domain;

namespace Core.Common.DataAccess.SharedEntities.Memories
{
    public class MemoryEntity : MemoryBase
    {
        public Guid Id { get; init; }
        
        public PersonaEntity? PersonaEntity { get; init; }
    }
}
