using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.Server.Handlers
{
    public class PwaSubscriptionsCollectionHandler
    {
        private readonly List<WebPushNotificationSubscriptionDto> collection;

        public PwaSubscriptionsCollectionHandler()
        {
            collection = [];
        }

        public IEnumerable<WebPushNotificationSubscriptionDto> Subscriptions => [.. collection];

        public void Add(WebPushNotificationSubscriptionDto handle)
        {
            lock (collection)
            {
                collection.Add(handle);
            }
        }

        public void Remove(WebPushNotificationSubscriptionDto handle)
        {
            lock (collection)
            {
                collection.Remove(handle);
            }
        }

        public WebPushNotificationSubscriptionDto RemoveIfExists(WebPushNotificationSubscriptionDto dto)
        {
            var existing = Find(p => p.Auth == dto.Auth && p.P256dh == dto.P256dh);

            if (existing is null)
            {
                return dto;
            }

            Remove(existing);

            return existing;
        }

        public WebPushNotificationSubscriptionDto? Find(Predicate<WebPushNotificationSubscriptionDto> predicate)
        {
            return collection.Find(predicate);
        }
    }
}
