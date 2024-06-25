using Chat.Domain.Messages;
using Chat.Services.Interfaces;
using ChatUserLink.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Links;
using Common.DataAccess.SharedEntities.Mappers;
using Common.DataAccess.SharedEntities.Objects;

namespace Chat.CQRS.Queries
{
    public class AddMessageQuery
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserAssociationRepository _userAssociationRepository;

        public AddMessageQuery(IMessageRepository messageRepository, IUserAssociationRepository userAssociationRepository)
        {
            _messageRepository = messageRepository;
            _userAssociationRepository = userAssociationRepository;
        }

        public async Task<MessageViewModel> Handle(PostMessageRequest request, Guid userId)
        {
            MessageEntity message = request.ToEntity(userId);
            
            await _messageRepository.Add(message);

            UserMessageEntity link = new UserMessageEntity(userId, message.Id);
            await _userAssociationRepository.Add(link);
            
            return message.ToViewModel();
        }
    }
}
