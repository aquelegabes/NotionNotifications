namespace NotionNotifications.Integration.Models;

public class NotionSettings
{
    public string DatabaseId { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string Version { get; set; } = "2022-06-28";
}