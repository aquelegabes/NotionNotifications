using NotionNotifications.Domain.Entities;
using NotionNotifications.Integration.Models;

namespace NotionNotifications.Domain.Extensions
{
    public static class NotionResultExtensions
    {
        public static NotificationRoot ToNotificationRoot(
            this NotionResultModel result)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return new()
            {
                IntegrationId = result.Id,
                Id = result.Properties["ID"]["unique_id"]["number"].GetValue<int>(),
                Title = result.Properties["Título"]["title"].AsArray().First()["plain_text"].GetValue<string>(),
                AlreadyNotified = result.Properties["Já Notificado?"]["checkbox"].GetValue<bool>(),
                Occurence = GetOccurence(result.Properties["Repetição"]["select"]["name"].GetValue<string>()),
                EventDate = result.Properties["Data do evento"]["date"]["start"].GetValue<DateTimeOffset>(),
                Categories = result.Properties["Categorias"]["multi_select"].AsArray().Select(_ => _["name"].GetValue<string>()),
                LastUpdatedAt = result.LastEditedTime,
                CreatedAt = result.CreatedTime,
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public static IEnumerable<NotificationRoot> ToNotificationRoot(
            this IEnumerable<NotionResultModel> result)
        {
            return result.Select(ToNotificationRoot);
        }

        private static ENotificationOccurence GetOccurence(string value)
        {
            return value switch
            {
                "Anualmente" => ENotificationOccurence.Annually,
                "Mensalmente" => ENotificationOccurence.Monthly,
                "Semanalmente" => ENotificationOccurence.Weekly,
                "Diariamente" => ENotificationOccurence.Daily,
                "Único" => ENotificationOccurence.None,
                _ => ENotificationOccurence.None,
            };
        }
    }
}
