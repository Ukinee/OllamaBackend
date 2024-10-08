﻿using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Chat.CQRS.Queries.Done
{
    public class GetConversationsFromPersonaQuery
    {
        public IList<GeneralConversationViewModel> Execute(PersonaEntity personaEntity)
        {
            return personaEntity
                .Conversations
                .Select(x => x.ToGeneralViewModel())
                .ToList();
        }
    }
}
