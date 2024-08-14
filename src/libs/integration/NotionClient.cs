// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. LN46
#pragma warning disable CS8618 

using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using NotionNotifications.Integration.Models;

namespace NotionNotifications.Integration;

public class NotionClient : IDisposable
{
    private HttpClient _client;
    private JsonSerializerOptions _serializerOptions;
    private NotionSettingsModel _settings;
    private bool disposedValue;

    private void ConfigureClient(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient();
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        _client.DefaultRequestHeaders.Add("Notion-Version", _settings.Version);
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_settings.Secret}");
    }
    private void ConfigureSettings()
    {
        TextEncoderSettings encoderSettings = new();
        encoderSettings.AllowRanges(UnicodeRanges.All);
        _serializerOptions = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            Encoder = JavaScriptEncoder.Create(encoderSettings),
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        _settings = new()
        {
            DatabaseId = Environment.GetEnvironmentVariable("NOTION_DATABASEID")!,
            Secret = Environment.GetEnvironmentVariable("NOTION_API_SECRET")!,
            Version = Environment.GetEnvironmentVariable("NOTION_API_VERSION")!
        };
    }

    public NotionClient(
        IHttpClientFactory clientFactory)
    {
        ConfigureSettings();
        ConfigureClient(clientFactory);
    }

    public async Task<IEnumerable<NotionResultModel>> GetNotifications(
        NotionNotificationResultFilterModel filter,
        int pageSize = 100,
        string? startCursor = null)
    {
        var uri = NotionUris.QueryUri(_settings.DatabaseId);
        var bodyContent = new
        {
            filter = filter.ToIntegrationFilter(pageSize, startCursor)
        };
        HttpContent body = JsonContent.Create(bodyContent, options: _serializerOptions);

        var response = await _client.PostAsync(uri, body);
        var content = await response.Content.ReadAsStringAsync();
        var jsonResult = JsonNode.Parse(content)!.AsObject();

        if (response.StatusCode == System.Net.HttpStatusCode.OK
            && jsonResult.ContainsKey("results")
            && jsonResult["results"] is not null)
        {
            var resultsArr = jsonResult["results"];
            var result =
                resultsArr.Deserialize<IEnumerable<NotionResultModel>>(_serializerOptions);
            return result ?? [];
        }

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new HttpRequestException(content);

        return [];
    }

    public async Task<HttpResponseMessage> AddNotification(NotionResultModel model)
    {
        var uri = NotionUris.PageUri();

        var bodyContent = new
        {
            parent = new { database_id = _settings.DatabaseId },
            properties = model.Properties
        };
        var body = JsonContent.Create(bodyContent, options: _serializerOptions);

        var result = await _client.PostAsync(uri, body);
        return result;
    }

    public async Task<HttpResponseMessage> UpdateNotification(NotionResultModel model)
    {
        var uri = NotionUris.PageUri(model.Id.ToString());

        var bodyContent = new
        {
            properties = model.Properties
        };
        var body = JsonContent.Create(bodyContent, options: _serializerOptions);

        return await _client.PatchAsync(uri, body);
    }

    public async Task<HttpResponseMessage> DeleteNotification(string pageId)
    {
        var uri = NotionUris.PageUri(pageId);

        var bodyContent = new
        {
            archived = true
        };
        var body = JsonContent.Create(bodyContent, options: _serializerOptions);

        return await _client.PatchAsync(uri, body);
    }

    public async Task<NotionDabaseInfoModel> GetDatabaseInfo()
    {
        var uri = NotionUris.DatabaseUri(_settings.DatabaseId);

        var response = await _client.GetAsync(uri);
        var content = await response.Content.ReadFromJsonAsync<NotionDabaseInfoModel>();
        return content!;
    }

    #region Dispose
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _client.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~NotionClient()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
