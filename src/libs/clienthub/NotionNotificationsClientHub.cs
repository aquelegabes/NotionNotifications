using Microsoft.AspNetCore.SignalR.Client;
using NotionNotifications.Domain;
using NotionNotifications.Domain.Dtos;

namespace NotionNotifications;

public class NotionNotificationsClientHub : IDisposable
{
    private readonly HubConnection _connection;
    private readonly INotificationHandler _handler;
    private const string NOTIFICATION_HUB = "/notification";
    private static string APP_URL => Environment.GetEnvironmentVariable("APP_URL")!;

    public NotionNotificationsClientHub(
        INotificationHandler handler)
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
    }

    public async Task Connect()
    {
        await _connection.StartAsync();

        while (!IsConnected())
        {
            Console.WriteLine("[*] Waiting for connection...");
            await Task.Delay(1000);
        }

        Console.WriteLine("[*] Connected: {0}", _connection.ConnectionId);

        ConfigureHandlers();
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
        _connection.StopAsync();
        Task.FromResult(_connection.DisposeAsync()).Wait();
    }
}