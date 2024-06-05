namespace NotionNotifications.Domain.Entities;

public class NotionObjectRoot {
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public bool IsNotification { get; set; } = true;
  public bool AlreadyNotified { get; set; } = false;
  public DateTimeOffset LastUpdatedAt { get; set; } = DateTimeOffset.UtcNow;
  public ENotificationOccurence Occurence { get; set; } = ENotificationOccurence.None;
}
