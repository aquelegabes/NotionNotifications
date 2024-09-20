using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.PWA
{
    public class NotificationCenterService(
        NotionNotificationsPwaClientHub client) : IDisposable
    {
        private async Task ConnectIfNotConnected()
        {
            if (!client.IsConnected())
                await client.Connect();
        }

        public IEnumerable<SimpleNotificationDto> GetClientNotifications() => client.GetClientNotifications();

        public async Task<bool> CheckSubscription(WebPushNotificationSubscriptionDto subscription)
        {
            await ConnectIfNotConnected();

            await client.CheckSubscriptionOnServer(subscription);
            return client.IsSubscribed;
        }

        public async Task Subscribe(
            WebPushNotificationSubscriptionDto subscription)
        {
            await ConnectIfNotConnected();

            await client.SubscribeToServer(subscription);
        }

        public async Task Unsubscribe(
            WebPushNotificationSubscriptionDto subscription)
        {
            await ConnectIfNotConnected();

            await client.UnsubscribeFromServer(subscription);
        }

        public async Task NotifySubscription(
            NotificationDto notification,
            WebPushNotificationSubscriptionDto subscription)
        {
            if (!client.IsConnected())
                await client.Connect();

            await client.NotifyCurrentClient(notification, subscription);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
