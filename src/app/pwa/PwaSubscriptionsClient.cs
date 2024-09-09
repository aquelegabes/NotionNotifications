using NotionNotifications.Domain.Dtos;
using System.Net.Http.Json;

namespace NotionNotifications.PWA
{
    public class PwaSubscriptionsClient(
        HttpClient client)
    {
        public async Task<WebPushNotificationSubscriptionDto> Subscribe(
            WebPushNotificationSubscriptionDto model)
        {
            var response = await client.PutAsJsonAsync("subscribe", model);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<WebPushNotificationSubscriptionDto>();
        }

        public async Task Unsubscribe(
            WebPushNotificationSubscriptionDto model)
        {
            var response = await client.PutAsJsonAsync("unsubscribe", model);
            response.EnsureSuccessStatusCode();
        }

        public async Task SendTestNotification(
            NotificationDto model)
        {
            await client.PostAsJsonAsync("pwa-test-notification", model);
        }
    }
}
