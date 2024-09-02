namespace NotionNotifications.Domain;

public interface INotificationHandler : IDisposable
{
    void Send(string title, string message, string icon = "");
}