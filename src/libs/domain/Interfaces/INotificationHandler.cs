namespace NotionNotifications.Domain;

public interface INotificationHandler
{
    void Send(string title, string message, string icon = "");
}