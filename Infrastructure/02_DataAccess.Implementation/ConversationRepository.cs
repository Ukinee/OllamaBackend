using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class ConversationRepository(MainRepository mainRepository) : IConversationRepository
    {
        public async Task<List<DatabaseConversationDto>> GetAllAsync() =>
             await mainRepository
                 .Conversations
                 .Include(dto => dto.Messages)
                 .ToListAsync();

        public async Task<DatabaseConversationDto?> FindConversationByIdAsync(Guid id) =>
            await mainRepository
                .Conversations
                .Include(dto => dto.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(DatabaseConversationDto conversation) =>
            await mainRepository.Conversations.AddAsync(conversation);

        public Task RemoveAsync(DatabaseConversationDto conversation)
        {
            mainRepository.Conversations.Remove(conversation);
            
            return Task.CompletedTask;
        }

        public async Task SaveAsync() =>
            await mainRepository.SaveChangesAsync();
    }
}
