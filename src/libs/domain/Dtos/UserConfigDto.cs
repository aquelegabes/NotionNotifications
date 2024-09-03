namespace NotionNotifications.Domain.Dtos;

public class UserConfigDto
{
    public TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;
}