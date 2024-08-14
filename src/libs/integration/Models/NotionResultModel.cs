using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace NotionNotifications.Integration.Models
{
    public class NotionResultModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("created_time")]
        public DateTimeOffset CreatedTime { get; set; }
        [JsonPropertyName("last_edited_time")]
        public DateTimeOffset LastEditedTime { get; set; }
        [JsonPropertyName("properties")]
        public JsonNode? Properties { get; set; }
    }
}
