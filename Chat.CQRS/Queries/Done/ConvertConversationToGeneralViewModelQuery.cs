﻿using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;

namespace Chat.CQRS.Queries.Done
{
    public class ConvertConversationToGeneralViewModelQuery
    {
        public GeneralConversationViewModel Execute(ConversationEntity conversation)
        {
            return conversation.ToGeneralViewModel();
        }
    }
}
