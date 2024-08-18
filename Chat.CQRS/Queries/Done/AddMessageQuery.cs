using Chat.DataAccess.Interfaces;
using Chat.Domain.Messages;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Chat.CQRS.Queries.Done
{
    public class AddMessageQuery
    {
        private readonly IMessageRepository _messageRepository;

        public AddMessageQuery(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageViewModel> Handle(PostMessageRequest request, PersonaEntity personaEntity)
        {
            MessageEntity message = request.ToEntity(personaEntity);
            
            await _messageRepository.Add(message);
            
            return message.ToViewModel();
        }
    }
}
