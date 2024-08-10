using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Chats.Mappers;
using Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Chat.CQRS.Queries
{
    public class AddConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IPersonasRepository _personasRepository;

        public AddConversationQuery(IConversationRepository conversationRepository, IPersonasRepository personasRepository)
        {
            _conversationRepository = conversationRepository;
            _personasRepository = personasRepository;
        }

        public async Task<GeneralConversationViewModel> Handle(PostConversationRequest request)
        {
            PersonaEntity? ownerPersona = await _personasRepository.Get(request.PersonaId);
            
            if(ownerPersona is null)
                throw new InvalidOperationException($"Persona with Id {request.PersonaId} not found"); //todo: hardcode

            if (ownerPersona.IsDeleted)
                throw new InvalidOperationException($"Persona with Id {request.PersonaId} was not found"); //todo: hardcode
            
            ConversationEntity conversationEntity = request.ToEntity(ownerPersona);

            await _conversationRepository.Add(conversationEntity);

            return conversationEntity.ToGeneralConversationViewModel();
        }
    }
}
