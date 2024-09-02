using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.Domain.Interfaces
{
    public interface IClientHandler
    {
        Task SendNotificationToClients(NotificationDto dto);
    }
}
