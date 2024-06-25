using Authorization.Services.Interfaces;
using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Chat.CQRS.Commands
{
    public class DeleteConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public DeleteConversationCommand
        (
            IConversationRepository conversationRepository,
            IMessageRepository messageRepository,
            IUserRepository userRepository
        )
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task Execute(Guid id)
        {
            ConversationEntity? conversation = await _conversationRepository.Get(id);

            if (conversation == null)
                throw new NotFoundException(nameof(ConversationEntity));

            await _conversationRepository.Delete(conversation);
            await _messageRepository.DeleteByConversationId(id);
        }
    }
}
