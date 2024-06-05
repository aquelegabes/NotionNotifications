namespace NotionNotifications.Domain.Entities;

public class TransactionEventRoot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public NotionObjectRoot OldObjectRoot { get; set; } = new();
    public NotionObjectRoot NewObjectRoot { get; set; } = new();
    public DateTimeOffset EventDate { get; set; } = DateTimeOffset.Now;
}