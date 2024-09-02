using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NotionNotifications.Domain.Entities;
using NotionNotifications.Domain.Extensions;
using NotionNotifications.Integration;
using NotionNotifications.Server.Hubs;

namespace NotionNotifications.Server.Jobs;

public class NotionIntegrationJobs(
    NotionClient client)
{

    public async Task SetNextNotificationOccurrence(
        NotificationRoot root)
    {
        await SetNotificationAsAlreadyNotified(root);

        if (root.Occurence != Domain.ENotificationOccurence.None)
        {
            var nextNotification = root.GenerateNextOccurrence();
            var model = nextNotification.ToNotionResultModel();
#if DEBUG
            var response = await client.AddNotification(model);
#endif
        }
    }

    public async Task SetNotificationAsAlreadyNotified(
        NotificationRoot notification)
    {
        notification.AlreadyNotified = true;
        var model = notification.ToNotionResultModel();
#if DEBUG
        var response = await client.UpdateNotification(model);
#endif
    }

    public async Task FetchAvailableNotificationsForCurrentDate()
    {
        var ctokenSource = new CancellationTokenSource();
        try
        {
            var now = DateTime.UtcNow;

            var notifications = await client.GetNotifications(
                filter: new()
                {
                    EventDate = now.Date
                }
            );

            foreach (var notification in notifications)
            {
                var notificationRoot = notification.ToNotificationRoot();
                ScheduleToSendNotification(notificationRoot);
            }
        }
        catch
        {
            ctokenSource.Cancel();
            throw;
        }
    }

    public void ScheduleToSendNotification(
        NotificationRoot root,
        CancellationToken cToken = default)
    {
        var timeToNotify = DateTimeOffset.Now - root.EventDate;
        // BackgroundJob.Schedule(() => hub.SendToClients(root, cToken), timeToNotify);
    }
}