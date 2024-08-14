namespace NotionNotifications.Integration;

public static class NotionUris
{
    private const string BASE_URL = "https://api.notion.com/v1/";
    private const string DATABASE_PATH = "databases/{0}";
    private const string QUERY_PATH = $"{DATABASE_PATH}/query";
    private const string PAGES_PATH = "pages";

    public static string QueryUri(string databaseId) =>
        Path.Join(BASE_URL, string.Format(QUERY_PATH, databaseId));
    public static string DatabaseUri(string databaseId) =>
        Path.Join(BASE_URL, string.Format(DATABASE_PATH, databaseId));
    public static string PageUri(string pageId) =>
        Path.Join(BASE_URL, $"{PAGES_PATH}/{pageId}");
    public static string PageUri() =>
        Path.Join(BASE_URL, PAGES_PATH);
}