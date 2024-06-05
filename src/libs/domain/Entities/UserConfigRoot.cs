namespace NotionNotifications.Domain.Entities;

public class UserConfigRoot {
    public TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;
    
}