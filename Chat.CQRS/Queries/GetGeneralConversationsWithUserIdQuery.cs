using Chat.DataAccess.Interfaces;
using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Core.Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Chat.CQRS.Queries
{
    public class GetGeneralConversationsWithUserIdQuery
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IPersonaRepository _personaRepository;

        public GetGeneralConversationsWithUserIdQuery
        (
            IConversationRepository conversationRepository,
            IPersonaRepository personaRepository
        )
        {
            _conversationRepository = conversationRepository;
            _personaRepository = personaRepository;
        }

        public async Task<IList<GeneralConversationViewModel>> Execute(Guid personaId)
        {
            PersonaEntity? persona = await _personaRepository.GetWithConversations(personaId);
            
            ArgumentNullException.ThrowIfNull(persona);
            
            return persona.Conversations.Select(conversation => conversation.ToGeneralConversationViewModel()).ToList();
        }
    }
}
