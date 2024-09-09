using Microsoft.AspNetCore.SignalR;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Domain.Interfaces;
using NotionNotifications.Server.Handlers;

namespace NotionNotifications.Server.Hubs
{
    public class HubHelper(
        IHubContext<NotionNotificationHub> hubContext,
        NotificationCollectionHandler collectionHandler) : IClientHandler
    {
        public async Task SendNotificationToClients(
            NotificationDto dto)
        {
            var handle = collectionHandler.Find(p => p.Notification.IntegrationId == dto.IntegrationId);

            if (handle is null || handle.IsFired)
                return;

            await hubContext.Clients.All.SendAsync(
                method: "OnNotify",
                arg1: dto,
                cancellationToken: CancellationToken.None);

            collectionHandler.Remove(handle);

            handle.IsFired = true;
            handle.IsScheduled = false;

            collectionHandler.Add(handle);
        }
    }
}
