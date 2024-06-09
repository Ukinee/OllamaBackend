using Chat.Domain.Messages;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Mappers;

namespace Chat.CQRS.Queries
{
    public class AddMessageQuery
    {
        private readonly IMessageRepository _messageRepository;

        public AddMessageQuery(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageViewModel> Handle(PostMessageRequest request, Guid userId)
        {
            MessageEntity message = request.ToEntity(userId);
            
            await _messageRepository.Add(message);
            
            return message.ToViewModel();
        }
    }
}
