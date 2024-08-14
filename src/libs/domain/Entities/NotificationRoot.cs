namespace NotionNotifications.Domain.Entities;

public class NotificationRoot
{
    public Guid IntegrationId { get; set; } = Guid.Empty;
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool AlreadyNotified { get; set; } = false;
    public DateTimeOffset EventDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastUpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ENotificationOccurence Occurence { get; set; } = ENotificationOccurence.None;
    public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();
}
