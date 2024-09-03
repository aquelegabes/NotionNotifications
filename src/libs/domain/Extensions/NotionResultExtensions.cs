using NotionNotifications.Domain.Dtos;
using NotionNotifications.Integration.Models;

namespace NotionNotifications.Domain.Extensions
{
    public static class NotionResultExtensions
    {
        public static NotificationDto ToNotificationDto(
            this NotionResultModel model)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return new()
            {
                IntegrationId = model.Id,
                NotionIdProperty = model.Properties["ID"]["unique_id"]["number"].GetValue<int>(),
                Title = model.Properties["Título"]["title"].AsArray().First()["plain_text"].GetValue<string>(),
                AlreadyNotified = model.Properties["Já Notificado?"]["checkbox"].GetValue<bool>(),
                Occurence = GetOccurence(model.Properties["Repetição"]["select"]["name"].GetValue<string>()),
                EventDate = model.Properties["Data do evento"]["date"]["start"].GetValue<DateTimeOffset>(),
                Categories = model.Properties["Categorias"]["multi_select"].AsArray().Select(_ => _["name"].GetValue<string>()).ToArray(),
                LastUpdatedAt = model.LastEditedTime,
                CreatedAt = model.CreatedTime,
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public static IEnumerable<NotificationDto> ToNotificationDto(
            this IEnumerable<NotionResultModel> models)
        {
            return models.Select(ToNotificationDto);
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
