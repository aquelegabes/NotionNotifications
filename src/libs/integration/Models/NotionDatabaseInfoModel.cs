namespace NotionNotifications.Integration.Models;

public class NotionDabaseInfoModel
{
    public string Id { get; set; } = string.Empty;
    public IDictionary<string, object> Properties { get; set; } =
        new Dictionary<string, object>();
}