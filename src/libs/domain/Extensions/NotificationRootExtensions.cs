using NotionNotifications.Domain.Entities;
using NotionNotifications.Integration.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NotionNotifications.Domain.Extensions
{
    public static class NotificationRootExtensions
    {
        public static NotificationRoot GenerateNextOccurrence(
            this NotificationRoot previousNotification)
        {
            return new()
            {
                Title = previousNotification.Title,
                Categories = previousNotification.Categories,
                Occurence = previousNotification.Occurence,
                EventDate = previousNotification.EventDate.GetNextDateOccurrence(previousNotification.Occurence),
                LastUpdatedAt = previousNotification.LastUpdatedAt,
            };
        }

        private static void SetupProperties(
            NotificationRoot root,
            Dictionary<string, object> properties)
        {
            properties.Add("Título", new
            {
                title = new[] { new { text = new { content = root.Title } } }
            });

            properties.Add("Data do evento", new
            {
                date = new { start = root.EventDate }
            });

            properties.Add("Já Notificado?", new
            {
                checkbox = root.AlreadyNotified,
            });

            properties.Add("Repetição", new
            {
                select = new { name = root.Occurence.GetDescription() }
            });

            properties.Add("É notificação?", new
            {
                checkbox = true
            });

            if (root.Categories.Any())
            {
                properties.Add("Categorias", new
                {
                    multi_select = root.Categories.Select(_ => new
                    {
                        name = _
                    })
                });
            }
        }

        public static NotionResultModel ToInsertNotionResultModel(
            this NotificationRoot root)
        {
            Dictionary<string, object> properties = [];

            SetupProperties(root, properties);

            string propertiesAsJson = JsonSerializer.Serialize(properties);

            return new NotionResultModel
            {
                Id = root.IntegrationId,
                CreatedTime = root.CreatedAt,
                LastEditedTime = root.LastUpdatedAt,
                Properties = JsonNode.Parse(propertiesAsJson)
            };
        }
    }
}
