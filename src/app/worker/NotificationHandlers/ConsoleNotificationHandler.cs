using NotionNotifications.Domain;

namespace NotionNotifications.WorkerService.NotificationHandlers
{
    public class ConsoleNotificationHandler : INotificationHandler
    {
        public void Send(string title, string message, string icon = "")
        {
            Console.WriteLine("Título: {0}\r\nMensagem: {1}\r\nÍcone: {2}", title, message, icon);
        }
    }
}
