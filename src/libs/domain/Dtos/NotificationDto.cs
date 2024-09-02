namespace NotionNotifications.Domain.Dtos
{
    public class NotificationDto
    {
        public Guid IntegrationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Icon { get; set; }
    }
}
