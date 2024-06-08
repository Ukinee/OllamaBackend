﻿namespace Chat.Domain.Messages.Mappers
{
    public static class MessageExtensions
    {
        public static MessageEntity ToEntity(this PostMessageRequest message)
        {
            return new MessageEntity
            {
                Id = Guid.NewGuid(),
                ConversationDtoId = message.ConversationId,
                Content = message.Content,
                ChatName = message.ChatName,
                ChatRole = message.ChatRole,
                Images = message.Images,
                Timestamp = DateTime.UtcNow,
            };
        }

        public static MessageViewModel ToViewModel(this MessageEntity message)
        {
            return new MessageViewModel
            {
                Id = message.Id,
                Content = message.Content,
                Images = message.Images,
                ChatRole = message.ChatRole,
                ChatName = message.ChatName,
                Timestamp = message.Timestamp,
            };
        }
    }
}
