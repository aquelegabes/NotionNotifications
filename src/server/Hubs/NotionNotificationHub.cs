using Hangfire;
using Microsoft.AspNetCore.SignalR;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Server.Handlers;
using NotionNotifications.Server.Jobs;

namespace NotionNotifications.Server.Hubs;

public class NotionNotificationHub(
    ILogger<NotionNotificationHub> logger,
    PwaSubscriptionsHandler subscriptionsHandler) : Hub
{
    public async Task NotifyCurrentClient(
        NotificationDto notification,
        WebPushNotificationSubscriptionDto subscription)
    {
        await subscriptionsHandler.NotifyPwaSubscriptions(notification, subscription);
        await Clients.Caller.SendAsync("OnNotify", notification);
    }

    public async Task NotifyAllClients(
        NotificationDto dto,
        CancellationToken cToken = default)
    {
        await subscriptionsHandler.NotifyPwaSubscriptions(dto);
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

    public void SubscribePwaClient(
        WebPushNotificationSubscriptionDto subscription)
    {
        if (subscription is not null)
            subscriptionsHandler.AddIfNotExists(subscription);
    }

    public void UnsubscribePwaClient(
        WebPushNotificationSubscriptionDto subscription)
    {
        if (subscription is not null)
            subscriptionsHandler.RemoveIfExists(subscription);
    }

    public async Task IsPwaClientSubscribed(
        WebPushNotificationSubscriptionDto subscription)
    {
        var isSubscribed = subscriptionsHandler.IsSubscriptionAlive(subscription);

        await Clients.Caller.SendAsync("OnSubscriptionCheck", isSubscribed);
    }
}