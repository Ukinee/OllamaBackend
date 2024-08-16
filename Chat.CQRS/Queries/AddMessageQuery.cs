using Chat.DataAccess.Interfaces;
using Chat.Domain.Messages;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Core.Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Chat.CQRS.Queries
{
    public class AddMessageQuery
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IPersonaRepository _personaRepository;

        public AddMessageQuery(IMessageRepository messageRepository, IPersonaRepository personaRepository)
        {
            _messageRepository = messageRepository;
            _personaRepository = personaRepository;
        }

        public async Task<MessageViewModel> Handle(PostMessageRequest request)
        {
            PersonaEntity? personaEntity = await _personaRepository.Get(request.PersonaId);

            if (personaEntity == null)
                throw new InvalidOperationException("Persona does not exist");
            
            MessageEntity message = request.ToEntity(personaEntity);
            
            await _messageRepository.Add(message);
            
            return message.ToViewModel();
        }
    }
}
