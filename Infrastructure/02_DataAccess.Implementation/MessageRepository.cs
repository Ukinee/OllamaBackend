using DataAccess.Interfaces;
using Domain.Dto.DataBaseDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class MessageRepository(MainRepository mainRepository) : IMessageRepository
    {
        public async Task<DatabaseMessageDto?> FindMessageByIdAsync(Guid id) =>
            await mainRepository.Messages.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(DatabaseMessageDto message) =>
            await mainRepository.Messages.AddAsync(message);

        public Task RemoveAsync(DatabaseMessageDto message)
        {
            mainRepository.Messages.Remove(message);

            return Task.CompletedTask;
        }

        public async Task RemoveByOwnerAsync(Guid ownerId)
        {
            List<DatabaseMessageDto> messages = await FindMessagesByConversationAsync(ownerId);
            mainRepository.Messages.RemoveRange(messages);
        }

        public async Task SaveAsync() =>
            await mainRepository.SaveChangesAsync();

        public async Task<List<DatabaseMessageDto>> FindMessagesByConversationAsync(Guid conversationId)
        {
            return await mainRepository
                .Messages
                .Where(x => x.ConversationDtoId == conversationId)
                .ToListAsync();
        }
    }
}
