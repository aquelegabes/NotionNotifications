using Microsoft.AspNetCore.SignalR;
using NotionNotifications.Domain.Entities;

namespace NotionNotifications.Server.Hubs;

public class NotionNotificationHub : Hub
{
    public async Task TestMethod()
    {
        await Clients.All.SendAsync("name");
    }

    public async Task SendToClients(
        NotificationRoot root,
        CancellationToken cToken = default)
    {
        await Clients.All.SendAsync(
            method: "Notify", 
            arg1: root.ToJson(), 
            cancellationToken: cToken);
    }
}