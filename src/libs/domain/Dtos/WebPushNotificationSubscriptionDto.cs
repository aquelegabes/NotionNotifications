using System.ComponentModel.DataAnnotations;

namespace NotionNotifications.Domain.Dtos
{
    public class WebPushNotificationSubscriptionDto
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string P256dh { get; set; }
        [Required]
        public string Auth { get; set; }
        public string ClientIp { get; set; }
    }
}
