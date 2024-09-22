using Memories.CQRS.Queries;
using Memories.Domain;
using Memories.Services.Interfaces;

namespace Memories.Services.Implementations;

public class MemoriesService(CreateMemoryQuery createMemoryQuery) : IMemoriesService
{
    public async Task AddMemory(PostMemoryRequest request, CancellationToken cancellationToken)
    {
        
    }
}
