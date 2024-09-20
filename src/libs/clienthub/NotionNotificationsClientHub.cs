using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NotionNotifications.Domain;
using NotionNotifications.Domain.Dtos;
using System.Net.Sockets;

namespace NotionNotifications;

public class NotionNotificationsClientHub : IDisposable
{
    protected const string NOTIFICATION_HUB = "/notification";
    protected readonly INotificationHandler _handler;
    protected readonly ILogger<NotionNotificationsClientHub> _logger;
    protected readonly HubConnection _connection;

    public NotionNotificationsClientHub(
        IConfiguration configuration,
        INotificationHandler handler,
        ILogger<NotionNotificationsClientHub> logger)
    {
        var url = configuration.GetValue<string>("AppUrl");

        _connection = new HubConnectionBuilder()
            .WithUrl(url + NOTIFICATION_HUB)
            .Build();

        _connection.Closed += async (err) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await _connection.StartAsync();
            ConfigureHandlers();
        };

        _handler = handler;
        _logger = logger;
    }

    public IEnumerable<SimpleNotificationDto> GetClientNotifications() => _handler.Notifications.AsEnumerable();

    public async Task Connect()
    {
        try
        {
            await _connection.StartAsync();

            while (!IsConnected())
            {
                _logger.LogInformation("Waiting for connection...");
                await Task.Delay(1000);
            }

            _logger.LogInformation("Connected: {connectionId}", _connection.ConnectionId);

            ConfigureHandlers();
        }
        catch (HttpRequestException ex)
        when (ex.InnerException is SocketException innerEx
                && innerEx.SocketErrorCode == SocketError.NotConnected)
        {
            _logger.LogError("Host not available for connection");
            throw;
        }
    }

    public async Task Disconnect() => await _connection.StopAsync();
    public bool IsConnected() => !string.IsNullOrWhiteSpace(_connection.ConnectionId);

    protected virtual void ConfigureHandlers()
    {
        _connection.On<NotificationDto>("OnNotify", OnNotify);
    }

    protected virtual async Task OnNotify(NotificationDto dto)
    {
        _handler.Send(
            title: dto.Title,
            message: dto.Message,
            icon: dto.Icon);
        await Task.Delay(100);

        await _connection.InvokeAsync("SetNotificationAsAlreadyNotified", dto);
    }

    public void Dispose()
    {
        Task.FromResult(Disconnect()).Wait();
        Task.FromResult(_connection.DisposeAsync()).Wait();
    }
}