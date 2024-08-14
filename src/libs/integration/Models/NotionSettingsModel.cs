namespace NotionNotifications.Integration.Models;

public class NotionSettingsModel
{
    public string DatabaseId { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}