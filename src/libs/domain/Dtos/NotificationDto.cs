namespace NotionNotifications.Domain.Dtos
{
    public class NotificationDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Guid IntegrationId { get; set; } = Guid.Empty;
        public int NotionIdProperty { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool AlreadyNotified { get; set; }
        public DateTimeOffset EventDate { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public ENotificationOccurence Occurence { get; set; }
        public string[] Categories { get; set; } = [];
        public string Message { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
