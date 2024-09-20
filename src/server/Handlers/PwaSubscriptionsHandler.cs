using NotionNotifications.Domain.Dtos;
using System.Text.Json;
using WebPush;

namespace NotionNotifications.Server.Handlers
{
    public class PwaSubscriptionsHandler
    {
        private readonly List<WebPushNotificationSubscriptionDto> subscriptionCollection = [];

        public void AddIfNotExists(WebPushNotificationSubscriptionDto handle)
        {
            lock (subscriptionCollection)
            {
                var existing = Find(p => p.Auth == handle.Auth && p.P256dh == handle.P256dh);

                if (existing is not null)
                    return;

                subscriptionCollection.Add(handle);
            }
        }

        public WebPushNotificationSubscriptionDto RemoveIfExists(WebPushNotificationSubscriptionDto handle)
        {
            lock (subscriptionCollection)
            {
                var existing = Find(p => p.Auth == handle.Auth && p.P256dh == handle.P256dh);

                if (existing is null)
                {
                    return handle;
                }

                subscriptionCollection.Remove(existing);

                return existing;
            }
        }

        public WebPushNotificationSubscriptionDto? Find(Predicate<WebPushNotificationSubscriptionDto> predicate)
        {
            return subscriptionCollection.Find(predicate);
        }

        public async Task NotifyPwaSubscriptions(
            NotificationDto dto,
            params WebPushNotificationSubscriptionDto[] subscriptionsToNotify)
        {
            var vapidDetails = new VapidDetails(
                subject: Environment.GetEnvironmentVariable("VAPID_SUBJECT"),
                publicKey: Environment.GetEnvironmentVariable("VAPID_PUBLIC_KEY"),
                privateKey: Environment.GetEnvironmentVariable("VAPID_PRIVATE_KEY"));

            using var client = new WebPushClient();

            foreach (var subscription in subscriptionsToNotify)
            {
                var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
                var payload = JsonSerializer.Serialize(new
                {
                    title = dto.Title,
                    message = dto.Message,
                    icon = dto.Icon,
                });

                try
                {
                    await client.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                }
                catch (WebPushException ex)
                when (ex.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Gone)
                {
                    RemoveIfExists(subscription);
                }
            }
        }

        public async Task NotifyPwaSubscriptions(
            NotificationDto dto)
        {
            await NotifyPwaSubscriptions(dto, [.. subscriptionCollection]);
        }

        public bool IsSubscriptionAlive(
            WebPushNotificationSubscriptionDto subscription)
        {
            return subscriptionCollection.Any(s =>
                subscription.P256dh == s.P256dh && subscription.Auth == s.Auth);
        }
    }
}
