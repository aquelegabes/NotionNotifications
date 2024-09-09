using NotionNotifications.Domain;

namespace NotionNotifications.PWA
{
    public class PwaNotificationHandler : INotificationHandler
    {
        private readonly List<DeviceNotificationDto> _notificationOnThisDevice = [];

        public void Send(string title, string message, string icon = "")
        {
            _notificationOnThisDevice.Add(new DeviceNotificationDto { Title = title, Message = message, Icon = icon });
        }
    }

    public class DeviceNotificationDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
        public DateTimeOffset NotifiedAt { get; } = DateTimeOffset.Now;
    }
}
