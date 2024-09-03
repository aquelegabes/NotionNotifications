using NotionNotifications.Integration.Models;
using System.Text.Json.Nodes;
using System.Text.Json;
using NotionNotifications.Domain.Dtos;

namespace NotionNotifications.Domain.Extensions
{
    public static class NotificationDtoExtensions
    {
        public static NotificationDto GenerateNextOccurrence(
            this NotificationDto previousNotification)
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

        public static NotionResultModel ToNotionResultModel(this NotificationDto dto)
        {
            Dictionary<string, object> properties = [];

            SetupProperties(dto, properties);

            string propertiesAsJson = JsonSerializer.Serialize(properties);

            return new NotionResultModel
            {
                Id = dto.IntegrationId,
                CreatedTime = dto.CreatedAt,
                LastEditedTime = dto.LastUpdatedAt,
                Properties = JsonNode.Parse(propertiesAsJson)
            };
        }

        private static void SetupProperties(
            NotificationDto dto,
            Dictionary<string, object> properties)
        {
            properties.Add("Título", new
            {
                title = new[] { new { text = new { content = dto.Title } } }
            });

            properties.Add("Data do evento", new
            {
                date = new { start = dto.EventDate }
            });

            properties.Add("Já Notificado?", new
            {
                checkbox = dto.AlreadyNotified,
            });

            properties.Add("Repetição", new
            {
                select = new { name = dto.Occurence.GetDescription() }
            });

            properties.Add("É notificação?", new
            {
                checkbox = true
            });

            if (dto.Categories.Length != 0)
            {
                properties.Add("Categorias", new
                {
                    multi_select = dto.Categories.Select(_ => new
                    {
                        name = _
                    })
                });
            }
        }
    }
}
