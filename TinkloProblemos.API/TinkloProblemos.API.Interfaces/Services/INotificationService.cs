namespace TinkloProblemos.API.Interfaces.Services
{
    public interface INotificationService
    {
        bool SendNotification(string userId, int problemId, string content);
    }
}