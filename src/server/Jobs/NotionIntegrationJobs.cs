using Hangfire;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Domain.Extensions;
using NotionNotifications.Domain.Interfaces;
using NotionNotifications.Integration;
using NotionNotifications.Server.Handlers;

namespace NotionNotifications.Server.Jobs;

public class NotionIntegrationJobs(
    NotionClient client,
    NotificationCollectionHandler collectionHandler)
{
    public async Task SetNextNotificationOccurrence(
        NotificationDto dto)
    {
        await SetNotificationAsAlreadyNotified(dto);

        if (dto.Occurence != Domain.ENotificationOccurence.None)
        {
            var nextNotification = dto.GenerateNextOccurrence();
            var model = nextNotification.ToNotionResultModel();
#if DEBUG
            var response = await client.AddNotification(model);
#else
            await client.AddNotification(model);
#endif
            var beforeDeletion = collectionHandler.RemoveIfExists(new() { Notification = dto });

            beforeDeletion.IsNextOccurrenceSetted = true;

            collectionHandler.AddIfNotExists(beforeDeletion);
        }
    }

    public async Task SetNotificationAsAlreadyNotified(
        NotificationDto dto)
    {
        dto.AlreadyNotified = true;

        if (dto.IntegrationId != default)
        {
            var model = dto.ToNotionResultModel();
#if DEBUG
            var response = await client.UpdateNotification(model);
#else
            await client.UpdateNotification(model);
#endif
        }

        var beforeDeletion = collectionHandler.RemoveIfExists(new() { Notification = dto });

        beforeDeletion.IsFired = true;
        beforeDeletion.IsScheduled = false;

        collectionHandler.AddIfNotExists(beforeDeletion);
    }

    public async Task FetchAvailableNotificationsForCurrentDate()
    {
        var now = DateTime.UtcNow;

        var availableNotifications = await client.GetNotifications(
            filter: new()
            {
                EventDate = now.Date
            }
        );

        foreach (var notification in availableNotifications)
        {
            var dto = notification.ToNotificationDto();
            ScheduleNotification(dto);
        }
    }

    public void ScheduleNotification(NotificationDto dto)
    {
        var existing = collectionHandler.Find(p => p.Notification.IntegrationId == dto.IntegrationId);

        if (existing?.IsScheduled == true)
            return;

        var timeNow = DateTimeOffset.Now;

        if (dto.EventDate < timeNow)
            return;

        var timeToNotify = dto.EventDate - DateTimeOffset.Now;

        string jobId = BackgroundJob.Schedule<IClientHandler>(
            methodCall: (handler) => handler.SendNotificationToClients(dto),
            delay: timeToNotify
        );

        if (string.IsNullOrWhiteSpace(jobId))
            return;

        collectionHandler.AddIfNotExists(new()
        {
            Notification = dto,
            IsScheduled = true,
        });
    }
}