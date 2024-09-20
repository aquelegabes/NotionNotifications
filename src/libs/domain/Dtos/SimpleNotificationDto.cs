namespace NotionNotifications.Domain.Dtos
{
    public class SimpleNotificationDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
        public DateTimeOffset NotifiedAt { get; } = DateTimeOffset.Now;
    }
}