namespace NotionNotifications.External;

public interface INotificationHandler
{
    void Send(string icon, string title, string message);
}
