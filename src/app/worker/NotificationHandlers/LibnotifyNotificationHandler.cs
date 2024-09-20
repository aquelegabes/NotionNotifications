using NotionNotifications.Domain;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.External.Libnotify;

namespace NotionNotifications.WorkerService.NotificationHandlers;

public class LibnotifyNotificationHandler : INotificationHandler
{
    private readonly List<SimpleNotificationDto> _notifications = [];
    public IEnumerable<SimpleNotificationDto> Notifications => _notifications.AsEnumerable();

    public event EventHandler<SimpleNotificationDto> OnSend;

    public void Send(string title, string message, string icon = "")
    {
        var notification = new Notification(title, message, icon);
        var result = notification.Show();
        var simpleNotification = new SimpleNotificationDto { Message = message, Icon = icon, Title = title };
        OnSend(this, simpleNotification);
        _notifications.Add(simpleNotification);
    }
}