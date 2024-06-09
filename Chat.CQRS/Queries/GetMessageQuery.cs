using Chat.Domain.Messages;
using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;

namespace Chat.CQRS.Queries
{
    public class GetMessageQuery
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessageQuery(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        
        public async Task<MessageEntity> Execute(Guid id)
        {
            MessageEntity? user = await _messageRepository.Get(id);
            
            if(user == null)
                throw new NotFoundException(nameof(user));
            
            return user;
        }
    }
}
