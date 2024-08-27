using Microsoft.AspNetCore.SignalR;

namespace NotionNotifications.Server.Hubs;

public class NotionNotificationHub : Hub {
    public async Task TestMethod() {
        await Clients.All.SendAsync("name");
    }
}