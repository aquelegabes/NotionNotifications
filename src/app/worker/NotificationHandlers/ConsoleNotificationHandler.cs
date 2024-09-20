using NotionNotifications.Domain;
using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.WorkerService.NotificationHandlers
{
    public class ConsoleNotificationHandler : INotificationHandler
    {
        private readonly List<SimpleNotificationDto> _notifications = [];
        public IEnumerable<SimpleNotificationDto> Notifications => _notifications.AsEnumerable();

        public event EventHandler<SimpleNotificationDto> OnSend;

        public void Send(string title, string message, string icon = "")
        {
            Console.WriteLine("Título: {0}\r\nMensagem: {1}\r\nÍcone: {2}", title, message, icon);
            OnSend?.Invoke(this, new SimpleNotificationDto { Title = title, Message = message });
            _notifications.Add(new SimpleNotificationDto { Title = title, Icon = icon, Message = message });
        }
    }
}
