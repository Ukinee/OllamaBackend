using Core.Common.DataAccess.SharedEntities.Memories;
using Core.Common.DataAccess.SharedEntities.Users;
using Memories.DataAccess.Interfaces;
using Memories.Domain;
using Memories.Services.Factories;

namespace Memories.CQRS.Queries
{
    public class CreateMemoryQuery
    {
        private readonly IMemoryRepository _memoryRepository;
        private readonly MemoryFactory _memoryFactory;

        public CreateMemoryQuery(IMemoryRepository memoryRepository, MemoryFactory memoryFactory)
        {
            _memoryRepository = memoryRepository;
            _memoryFactory = memoryFactory;
        }
        
        public async Task<MemoryEntity> Execute(PersonaEntity owner, PostMemoryRequest request, CancellationToken cancellationToken)
        {
            MemoryEntity memoryEntity = _memoryFactory.Create(request, owner);

            await _memoryRepository.Add(memoryEntity, cancellationToken);

            return memoryEntity;
        }
    }
}
