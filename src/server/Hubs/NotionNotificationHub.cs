using Hangfire;
using Microsoft.AspNetCore.SignalR;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Server.Handlers;
using NotionNotifications.Server.Jobs;
using System.Text.Json;
using WebPush;

namespace NotionNotifications.Server.Hubs;

public class NotionNotificationHub(
    ILogger<NotionNotificationHub> logger,
    PwaSubscriptionsCollectionHandler subscriptionsHandler) : Hub
{

    public async Task NotifyAllClients(
        NotificationDto dto,
        CancellationToken cToken = default)
    {
        await NotifyPwaSubscriptions(dto);
        await Clients.All.SendAsync("OnNotify", dto, cToken);
    }

    public async Task SetNotificationAsAlreadyNotified(
        NotificationDto dto)
    {
        logger.LogInformation("CLIENT: {connectionId} NOTIFIED", Context.ConnectionId);
        logger.LogInformation("NOTIFICATION: {notificationObject}", dto.ToJson());
        await Task.Delay(100);

        BackgroundJob.Schedule<NotionIntegrationJobs>(
            methodCall: job => job.SetNextNotificationOccurrence(dto),
            delay: TimeSpan.FromMinutes(1));
    }

    private async Task NotifyPwaSubscriptions(
        NotificationDto dto)
    {
        var vapidDetails = new VapidDetails(
            subject: Environment.GetEnvironmentVariable("VAPID_SUBJECT"),
            publicKey: Environment.GetEnvironmentVariable("VAPID_PUBLIC_KEY"),
            privateKey: Environment.GetEnvironmentVariable("VAPID_PRIVATE_KEY"));

        using var client = new WebPushClient();

        foreach (var subscription in subscriptionsHandler.Subscriptions)
        {
            var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
            var payload = JsonSerializer.Serialize(new
            {
                title = dto.Title,
                message = dto.Message,
                icon = dto.Icon,
            });

            try
            {
                await client.SendNotificationAsync(pushSubscription, payload, vapidDetails);
            }
            catch (WebPushException ex)
            when (ex.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Gone)
            {
                subscriptionsHandler.Remove(subscription);
            }
        }
    }
}