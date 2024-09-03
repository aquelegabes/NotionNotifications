using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.Server;

public class NotificationCollectionHandler
{
    private readonly List<NotificationHandle> handles;

    public NotificationCollectionHandler()
    {
        handles = [];
    }

    public void Add(NotificationHandle handle)
    {
        lock (handles)
        {
            handles.Add(handle);
        }
    }

    public void Remove(NotificationHandle handle)
    {
        lock (handles)
        {
            handles.Remove(handle);
        }
    }

    public NotificationHandle RemoveIfExists(NotificationDto dto)
    {
        var existing = Find(p => p.Notification.IntegrationId == dto.IntegrationId);

        if (existing is null)
        {
            return new()
            {
                Notification = dto,
            };
        }

        Remove(existing);

        return existing;
    }

    public NotificationHandle? Find(Predicate<NotificationHandle> predicate)
    {
        return handles.Find(predicate);
    }
}

public class NotificationHandle
{
    public NotificationDto Notification { get; set; } = new();
    public bool IsScheduled { get; set; }
    public bool IsFired { get; set; }
    public bool IsNextOccurrenceSetted { get; set; }
}