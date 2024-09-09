namespace NotionNotifications.Domain.Dtos
{
    public class WebPushNotificationSubscriptionDto
    {
        public string UserId { get; set; }
        public string Url { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
    }
}
