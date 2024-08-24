using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NotionNotifications.Domain.Entities;

public class TransactionEventRoot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset EventDate { get; set; } = DateTimeOffset.Now;
    public string OldObjectAsJsonString { get; set; }
    public string NewObjectAsJsonString { get; set; }

    [NotMapped]
    public NotificationRoot OldObjectRoot => JsonSerializer.Deserialize<NotificationRoot>(OldObjectAsJsonString) ?? new();
    [NotMapped]
    public NotificationRoot NewObjectRoot => JsonSerializer.Deserialize<NotificationRoot>(NewObjectAsJsonString) ?? new();
}