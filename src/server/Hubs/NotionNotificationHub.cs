using Hangfire;
using Microsoft.AspNetCore.SignalR;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Server.Jobs;

namespace NotionNotifications.Server.Hubs;

public class NotionNotificationHub(ILogger<NotionNotificationHub> logger) : Hub
{
    public async Task NotifyAllClients(
        NotificationDto dto,
        CancellationToken cToken = default)
    {
        await Clients.All.SendAsync("OnNotify", dto, cToken);
    }

    public async Task SetNotificationAsAlreadyNotified(
        NotificationDto dto)
    {
        logger.LogInformation("CLIENT: {0} NOTIFIED", Context.ConnectionId);
        logger.LogInformation("NOTIFICATION: {0}", dto.ToJson());
        await Task.Delay(100);

        BackgroundJob.Schedule<NotionIntegrationJobs>(
            methodCall: job => job.SetNextNotificationOccurrence(dto),
            delay: TimeSpan.FromMinutes(1));
    }
}