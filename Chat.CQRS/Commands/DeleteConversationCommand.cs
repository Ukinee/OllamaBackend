using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Chat.CQRS.Commands
{
    public class DeleteConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;

        public DeleteConversationCommand
        (
            IConversationRepository conversationRepository,
            IMessageRepository messageRepository
        )
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
        }

        public async Task Execute(Guid id)
        {
            ConversationEntity? conversation = await _conversationRepository.FindConversationById(id);

            if (conversation == null)
                throw new NotFoundException(nameof(ConversationEntity));

            await _conversationRepository.Delete(conversation);
            await _messageRepository.DeleteByConversationId(id);
        }
    }
}
