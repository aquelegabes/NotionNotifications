using NotionNotifications.Domain.Entities;
using NotionNotifications.Domain.Extensions;
using NotionNotifications.Integration;

namespace NotionNotifications.Domain.Services
{
    public class NotificationRootService
    {
        private readonly NotionClient _client;

        public NotificationRootService(NotionClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<NotificationRoot>> GetDailyNotifications()
        {
            var result = await _client.GetNotifications(new());
            return result.ToNotificationRoot();
        }
    }
}
