using Core.Common.DataAccess.SharedEntities.Memories;
using Core.Common.DataAccess.SharedEntities.Users;
using Memories.Domain;

namespace Memories.Services.Factories;

public class MemoryFactory
{
    public MemoryEntity Create(PostMemoryRequest postMemoryRequest, PersonaEntity personaEntity)
    {
        return new MemoryEntity()
        {
            Id = Guid.NewGuid(),
            Content = postMemoryRequest.Content,
            Topic = postMemoryRequest.Topic,
            IsConfirmed = postMemoryRequest.IsConfirmed,
            PersonaId = personaEntity.Id,
            PersonaEntity = personaEntity,
        };
    }
}
