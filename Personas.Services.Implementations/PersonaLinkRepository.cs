using Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using Persona.Models;
using Personas.Services.Interfaces;

namespace Personas.Services.Implementations
{
    public class PersonaLinkRepository : IPersonaLinkRepository
    {
        private readonly UserDbContext _userDbContext;

        public PersonaLinkRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        
        public Task Add(PersonaLinkEntity entity)
        {
            _userDbContext.PersonaLinks.Add(entity);
            return _userDbContext.SaveChangesAsync();
        }

        public Task<PersonaLinkEntity?> Get(Guid conversationId, Guid userId)
        {
            return _userDbContext.PersonaLinks
                .FirstOrDefaultAsync(x => x.ConversationId == conversationId && x.UserId == userId);
        }

        public Task Update(PersonaLinkEntity entity)
        {
            _userDbContext.PersonaLinks.Update(entity);
            return _userDbContext.SaveChangesAsync();
        }

        public Task Delete(Guid userId, Guid conversationId)
        {
            throw new NotImplementedException();
        }
    }
}
