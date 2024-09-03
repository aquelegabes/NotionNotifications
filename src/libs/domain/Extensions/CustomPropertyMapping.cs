using System.Reflection;

namespace NotionNotifications.Domain.Extensions;

public class CustomPropertyMapping
{
    public string Name { get; set; } = "";
    public Action<PropertyInfo, object, object>? PropertyAction { get; set; }
}