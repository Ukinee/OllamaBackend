namespace Users.FakeUsers.Services.Interfaces
{
    public interface ISystemMessageService
    {
        public Task SendSystemMessage(string content, Guid conversationId);
    }
}
