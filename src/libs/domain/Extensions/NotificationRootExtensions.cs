using NotionNotifications.Domain.Dtos;
using NotionNotifications.Domain.Entities;
using NotionNotifications.Integration.Models;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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

        public static NotionResultModel ToNotionResultModel(
            this NotificationRoot root,
            IEnumerable<string>? updatedProperties = default)
        {
            Dictionary<string, object> properties = [];

            SetupProperties(root, properties);

            if (updatedProperties?.Any() == true)
            {
                SetupUpdatedProperties(properties, updatedProperties!);
            }

            string propertiesAsJson = JsonSerializer.Serialize(properties);

            return new NotionResultModel
            {
                Id = root.IntegrationId,
                CreatedTime = root.CreatedAt,
                LastEditedTime = root.LastUpdatedAt,
                Properties = JsonNode.Parse(propertiesAsJson)
            };
        }

        public static NotificationDto ToNotificationDto(
            this NotificationRoot root,
            string message = "",
            string icon = "")
        {
            var result = new NotificationDto()
            {
                Message = message,
                Icon = icon
            };

            root.MapPropertiesTo(result, new CustomPropertyMapping
            {
                Name = nameof(NotificationDto.Categories),
                PropertyAction = (destinationPropInfo, propSource, destination) =>
                {
                    if (destinationPropInfo.PropertyType.FullName == typeof(string[]).FullName
                        && propSource is IEnumerable<string> enumerable)
                    {
                        var arr = enumerable.ToArray();
                        destinationPropInfo.SetValue(destination, arr);
                    }
                }
            });

            return result;
        }

        private static void SetupUpdatedProperties(
            Dictionary<string, object> properties,
            IEnumerable<string> updatedProperties)
        {
            var propertyMap = new Dictionary<string, string>
            {
                { nameof(NotificationRoot.Title), "Título" },
                { nameof(NotificationRoot.AlreadyNotified), "Já Notificado?" },
                { nameof(NotificationRoot.Categories), "Categorias" },
                { nameof(NotificationRoot.EventDate), "Data do evento" },
                { nameof(NotificationRoot.Occurence), "Repetição" }
            };

            foreach (var kvp in propertyMap)
            {
                if (!updatedProperties.Contains(kvp.Key))
                {
                    properties.Remove(kvp.Value);
                }
            }
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

    }
}
