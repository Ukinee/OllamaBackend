using Chat.DataAccess.Interfaces;
using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Core.Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Chat.CQRS.Queries
{
    public class AddConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IPersonaRepository _personaRepository;

        public AddConversationQuery(IConversationRepository conversationRepository, IPersonaRepository personaRepository)
        {
            _conversationRepository = conversationRepository;
            _personaRepository = personaRepository;
        }

        public async Task<GeneralConversationViewModel> Handle(PostConversationRequest request)
        {
            PersonaEntity? ownerPersona = await _personaRepository.Get(request.PersonaId);
            
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
