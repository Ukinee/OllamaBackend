namespace Users.FakeUsers.Services.Interfaces
{
    public interface IFakeUserService
    {
        public Task SendSystemMessage(string content, Guid conversationId);
    }
}
