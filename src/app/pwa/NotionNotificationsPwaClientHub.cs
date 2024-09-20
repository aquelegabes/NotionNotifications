using NotionNotifications.Domain.Dtos;
using NotionNotifications.Domain;
using Microsoft.AspNetCore.SignalR.Client;

namespace NotionNotifications.PWA
{
    public class NotionNotificationsPwaClientHub : NotionNotificationsClientHub
    {
        public bool IsSubscribed { get; private set; }

        public NotionNotificationsPwaClientHub(
            IConfiguration configuration,
            INotificationHandler handler,
            ILogger<NotionNotificationsClientHub> logger)
            : base(configuration, handler, logger)
        {
        }

        public async Task SubscribeToServer(
            WebPushNotificationSubscriptionDto subscription)
        {
            await _connection.InvokeAsync("SubscribePwaClient", subscription);
            IsSubscribed = true;
        }

        public async Task UnsubscribeFromServer(
            WebPushNotificationSubscriptionDto subscription)
        {
            await _connection.InvokeAsync("UnsubscribePwaClient", subscription);
            IsSubscribed = false;
        }

        public async Task CheckSubscriptionOnServer(
            WebPushNotificationSubscriptionDto subscription)
        {
            await _connection.InvokeAsync("IsPwaClientSubscribed", subscription);
        }

        public async Task NotifyCurrentClient(
        NotificationDto notification,
            WebPushNotificationSubscriptionDto subscription)
        {
            await _connection.InvokeAsync("NotifyCurrentClient", notification, subscription);
        }

        protected override void ConfigureHandlers()
        {
            base.ConfigureHandlers();

            _connection.On<bool>("OnSubscriptionCheck", res => IsSubscribed = res);
        }
    }
}
