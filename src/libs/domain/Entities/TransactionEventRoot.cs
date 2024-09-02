using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace NotionNotifications.Domain.Entities;

public class TransactionEventRoot
{
    public TransactionEventRoot() { }
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset EventDate { get; set; } = DateTimeOffset.Now;
    public string OldObjectAsJsonString { get; set; } = "{}";
    public string NewObjectAsJsonString { get; set; } = "{}";
    public string? Description { get; set; } = null;
    public ETransactionType TransactionType { get; set; }

    [NotMapped]
    public NotificationRoot OldObjectRoot => JsonSerializer.Deserialize<NotificationRoot>(OldObjectAsJsonString) ?? new();
    [NotMapped]
    public NotificationRoot NewObjectRoot => JsonSerializer.Deserialize<NotificationRoot>(NewObjectAsJsonString) ?? new();

    public static TransactionEventRoot New(
        NotificationRoot newObject,
        ETransactionType transactionType,
        NotificationRoot? oldObject = null)
    {
        return new TransactionEventRoot()
        {
            NewObjectAsJsonString = newObject.ToJson(),
            OldObjectAsJsonString = oldObject is null ? "{}" : oldObject.ToJson(),
            TransactionType = transactionType,
        };
    }
}