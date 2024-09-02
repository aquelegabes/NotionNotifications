using Microsoft.AspNetCore.SignalR;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Domain.Interfaces;

namespace NotionNotifications.Server.Hubs
{
    public class HubHelper(IHubContext<NotionNotificationHub> hubContext) : IClientHandler
    {
        public async Task SendNotificationToClients(
            NotificationDto dto)
        {
            await hubContext.Clients.All.SendAsync(
                method: "Notify",
                arg1: dto,
                cancellationToken: CancellationToken.None);
        }
    }
}
