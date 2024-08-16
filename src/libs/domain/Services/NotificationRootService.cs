using NotionNotifications.Domain.Entities;
using NotionNotifications.Domain.Exceptions;
using NotionNotifications.Domain.Extensions;
using NotionNotifications.Integration;
using NotionNotifications.Integration.Models;

namespace NotionNotifications.Domain.Services
{
    public class NotificationRootService(NotionClient client)
    {
        private readonly NotionClient _client = client;

        public async Task<IEnumerable<NotificationRoot>> Get(
            NotionNotificationResultFilterModel filter)
        {
            var result = await _client.GetNotifications(filter);
            return result.ToNotificationRoot();
        }

        public async Task Update(NotificationRoot root)
        {
            var model = root.ToNotionResultModel();
            var response = await _client.UpdateNotification(model);

            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestExceptionException(response);
            }
        }

        public async Task Add(NotificationRoot root)
        {
            var model = root.ToNotionResultModel();
            var response = await _client.AddNotification(model);

            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestExceptionException(response);
            }
        }

        public async Task Remove(string id)
        {
            var response = await _client.DeleteNotification(id);

            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestExceptionException(response);
            }
        }
    }
}
