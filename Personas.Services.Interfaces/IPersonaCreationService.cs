using Core.Common.DataAccess.SharedEntities.Users;

namespace Personas.Services.Interfaces
{
    public interface IPersonaCreationService
    {
        public Task<PersonaEntity> Create(Guid userId, IdentityEntity identity, string name);
    }
}
