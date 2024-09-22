using Core.Common.DataAccess.SharedEntities.Memories;

namespace Memories.DataAccess.Interfaces;

public interface IMemoryRepository
{
    public Task Add(MemoryEntity memoryEntity, CancellationToken cancellationToken);
}
