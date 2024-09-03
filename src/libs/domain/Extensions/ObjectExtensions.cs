using System.Text.Json;
using NotionNotifications.Domain.Extensions;

public static class ObjectExtensions
{
    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static void MapPropertiesTo<TResult>(
        this object source,
        TResult destination,
        params CustomPropertyMapping[] customPropertyMappings)
    {
        if (destination is null)
            throw new ArgumentNullException(nameof(destination));

        var sourceProps = source.GetType().GetProperties();
        var dtoProps = typeof(TResult).GetProperties();

        foreach (var sourceProp in sourceProps)
        {
            var sourcePropValue = sourceProp.GetValue(source);

            if (sourcePropValue is not null
                && dtoProps.Any(dtoProp => dtoProp.Name == sourceProp.Name))
            {
                var currentProp = dtoProps.FirstOrDefault(dtoProp => dtoProp.Name == sourceProp.Name);

                if (currentProp is not null
                    && customPropertyMappings.Length != 0)
                {
                    var customMapping = customPropertyMappings.FirstOrDefault(p => p.Name == sourceProp.Name);
                    
                    if (customMapping is null)
                    {
                        currentProp.SetValue(destination, sourceProp.GetValue(source));
                        continue;
                    }

                    customMapping.PropertyAction?.Invoke(currentProp, sourcePropValue, destination);
                }
            }
        }
    }
}