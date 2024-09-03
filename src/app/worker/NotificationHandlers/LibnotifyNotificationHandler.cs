using NotionNotifications.Domain;
using NotionNotifications.External.Libnotify;

namespace NotionNotifications.WorkerService.NotificationHandlers;

public class LibnotifyNotificationHandler : INotificationHandler
{
    public void Send(string title, string message, string icon = "")
    {
        var notification = new Notification(title, message, icon);
        var result = notification.Show();
    }
}