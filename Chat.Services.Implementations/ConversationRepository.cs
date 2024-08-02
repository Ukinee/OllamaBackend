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
        private readonly ChatContext _userDbContext;

        public ConversationRepository(ChatContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<List<ConversationEntity>> GetGeneralConversations(Guid userId)
        {
            return await _userDbContext
                .Conversations
                .Where(conversationEntity => conversationEntity.OwnerId == userId)
                .ToListAsync();
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

            conversation.IsDeleted = true;

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
            conversation.GlobalContext = request.GlobalContext;
            
            await Save();
        }

        private async Task Save()
        {
            await _userDbContext.SaveChangesAsync();
        }
    }
}
