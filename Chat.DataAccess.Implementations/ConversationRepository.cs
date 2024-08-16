using Chat.DataAccess.Interfaces;
using Chat.Domain.Conversations;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Implementations
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly CompositeContext _userDbContext;

        public ConversationRepository(CompositeContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<ConversationEntity?> GetConcreteConversation(Guid conversationId)
        {
            ConversationEntity? conversationEntity = await _userDbContext
                .Conversations
                .Include(conversation => conversation.Personas)
                .Include(conversation => conversation.Messages)
                .ThenInclude(x => x.SenderPersona)
                .FirstOrDefaultAsync(x => x.Id == conversationId);
            
            return conversationEntity;
        }

        public async Task Add(ConversationEntity conversation)
        {
            await _userDbContext.Conversations.AddAsync(conversation);

            await Save();
        }

        public async Task Delete(Guid conversationId)
        {
            ConversationEntity? conversation = await GetConcreteConversation(conversationId);

            if (conversation == null)
                throw new NotFoundException(nameof(conversation));

            throw new NotImplementedException("Must not be implemented");

            _userDbContext.Conversations.Remove(conversation);

            await Save();
        }

        public async Task Update(ConversationEntity conversation)
        {
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
