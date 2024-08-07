using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Microsoft.EntityFrameworkCore;

namespace Chat.Services.Implementations
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly CompositeContext _userDbContext;

        public ConversationRepository(CompositeContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<List<ConversationEntity>> GetGeneralConversations(Guid personaId)
        {
            var query = _userDbContext
                .Conversations
                .Include(conversation => conversation.Personas)
                .Where(conversation => conversation.Personas.Any(x => x.Id == personaId));

                
            return await query.ToListAsync();
        }

        public async Task<ConversationEntity?> Get(Guid id)
        {
            ConversationEntity? conversationEntity = await _userDbContext
                .Conversations
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return conversationEntity;
        }

        public async Task Add(ConversationEntity conversation)
        {
            await _userDbContext.Conversations.AddAsync(conversation);

            await Save();
        }

        public async Task Delete(Guid conversationId)
        {
            ConversationEntity? conversation = await Get(conversationId);
            
            if(conversation == null)
                throw new NotFoundException(nameof(conversation));
            
            throw new NotImplementedException("Must not be implemented");
            
            _userDbContext.Conversations.Remove(conversation);

            await Save();
        }

        public async Task Update(PutConversationRequest request)
        {
            ConversationEntity? conversation = await _userDbContext
                .Conversations
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (conversation == null)
                throw new NotFoundException(nameof(conversation));
            
            conversation.Name = request.Name;
            conversation.Context = request.Context;
            
            await Save();
        }

        private async Task Save()
        {
            await _userDbContext.SaveChangesAsync();
        }
    }
}
