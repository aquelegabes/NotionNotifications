namespace NotionNotifications.Domain.Entities;

public class TransactionEventRoot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public NotificationRoot OldObjectRoot { get; set; } = new();
    public NotificationRoot NewObjectRoot { get; set; } = new();
    public DateTimeOffset EventDate { get; set; } = DateTimeOffset.Now;
}