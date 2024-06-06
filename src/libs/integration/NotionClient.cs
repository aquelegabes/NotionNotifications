using NotionNotifications.Integration.Models;

namespace NotionNotifications.Integration;

public class NotionClient
{
    private const string BASE_URL = "https://api.notion.com/v1/";
    private string QUERY_URL = string.Join(BASE_URL, "database/{0}/query");
    private readonly HttpClient _client;
    private readonly NotionSettings _settings;

    public NotionClient(
        IHttpClientFactory clientFactory,
        NotionSettings settings)
    {
        _client = clientFactory.CreateClient();
        _settings = settings;
        ConfigureClientHeaders();
    }

    private void ConfigureClientHeaders()
    {
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        _client.DefaultRequestHeaders.Add("Notion-Version", _settings.Version);
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_settings.Secret}");
    }
}
