using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.Domain;

public interface INotificationHandler
{
    void Send(string title, string message, string icon = "");
    public IEnumerable<SimpleNotificationDto> Notifications { get; }
    event EventHandler<SimpleNotificationDto> OnSend;
}