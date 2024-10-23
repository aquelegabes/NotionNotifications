using NotionNotifications.Domain;
using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.PWA
{
    public class PwaNotificationHandler : INotificationHandler
    {
        public PwaNotificationHandler()
        {
            OnSend += (obj, args) => {};
        }

        private readonly List<SimpleNotificationDto> _notificationOnThisDevice = [];

        public IEnumerable<SimpleNotificationDto> Notifications => _notificationOnThisDevice.AsEnumerable();

        public event EventHandler<SimpleNotificationDto> OnSend;

        public void Send(string title, string message, string icon = "")
        {
            var deviceNotification = new SimpleNotificationDto { Title = title, Message = message, Icon = icon };
            _notificationOnThisDevice.Add(deviceNotification);
            this.OnSend?.Invoke(this, deviceNotification);
        }
    }
}
