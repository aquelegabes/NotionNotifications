using System.Text.Json;

public static class ObjectExtensions {
    public static string ToJson(this object obj) {
        return JsonSerializer.Serialize(obj);
    }
}