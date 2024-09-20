using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.Server.Handlers;

public class NotificationCollectionHandler
{
    private readonly List<NotificationHandle> collection = [];

    public void AddIfNotExists(NotificationHandle handle)
    {
        lock (collection)
        {
            var existing = Find(p => p.Notification.IntegrationId == handle.Notification.IntegrationId);

            if (existing is not null)
                return;

            collection.Add(handle);
        }
    }

    public NotificationHandle RemoveIfExists(NotificationHandle handle)
    {
        lock (collection)
        {
            var existing = Find(p => p.Notification.IntegrationId == handle.Notification.IntegrationId);

            if (existing is null)
            {
                return new()
                {
                    Notification = handle.Notification,
                };
            }

            collection.Remove(existing);

            return existing;
        }
    }

    public NotificationHandle? Find(Predicate<NotificationHandle> predicate)
    {
        return collection.Find(predicate);
    }
}

public class NotificationHandle
{
    public NotificationDto Notification { get; set; } = new();
    public bool IsScheduled { get; set; }
    public bool IsFired { get; set; }
    public bool IsNextOccurrenceSetted { get; set; }
}