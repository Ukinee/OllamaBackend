using Chat.Domain.Messages;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Chats.Mappers;
using Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Chat.CQRS.Queries
{
    public class AddMessageQuery
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IPersonasRepository _personasRepository;

        public AddMessageQuery(IMessageRepository messageRepository, IPersonasRepository personasRepository)
        {
            _messageRepository = messageRepository;
            _personasRepository = personasRepository;
        }

        public async Task<MessageViewModel> Handle(PostMessageRequest request)
        {
            PersonaEntity? personaEntity = await _personasRepository.Get(request.PersonaId);

            if (personaEntity == null)
                throw new InvalidOperationException("Persona does not exist");
            
            MessageEntity message = request.ToEntity();
            
            await _messageRepository.Add(message);
            
            return message.ToViewModel();
        }
    }
}
