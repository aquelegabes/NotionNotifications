using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using NotionNotifications.Domain;
using NotionNotifications.Domain.Dtos;
using System.Net.Sockets;

namespace NotionNotifications;

public class NotionNotificationsClientHub : IDisposable
{
    private readonly HubConnection _connection;
    private readonly INotificationHandler _handler;
    private readonly ILogger<NotionNotificationsClientHub> _logger;
    private const string NOTIFICATION_HUB = "/notification";
    private static string APP_URL => Environment.GetEnvironmentVariable("APP_URL")!;

    public NotionNotificationsClientHub(
        INotificationHandler handler,
        ILogger<NotionNotificationsClientHub> logger)
    {
        this._connection = new HubConnectionBuilder()
            .WithUrl($"{APP_URL}{NOTIFICATION_HUB}")
            .Build();

        _connection.Closed += async (err) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await _connection.StartAsync();
            ConfigureHandlers();
        };

        this._handler = handler;
        this._logger = logger;
    }

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

    private void ConfigureHandlers()
    {
        _connection.On<NotificationDto>("OnNotify", OnNotify);
    }

    public async Task OnNotify(NotificationDto dto)
    {
        _handler.Send(
            title: "Nova notificação",
            message: dto.Title);
        await Task.Delay(100);

        await _connection.InvokeAsync("SetNotificationAsAlreadyNotified", dto);
    }

    public void Dispose()
    {
        Task.FromResult(Disconnect()).Wait();
        Task.FromResult(_connection.DisposeAsync()).Wait();
    }
}