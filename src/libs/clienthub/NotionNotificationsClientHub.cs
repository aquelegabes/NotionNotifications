using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using NotionNotifications.Domain;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Domain.Entities;

namespace NotionNotifications;

public class NotionNotificationsClientHub : IDisposable
{
    private bool disposedValue;

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
        };
        this._handler = handler;
    }

    public async Task Connect()
    {
        try
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
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task Disconnect() => await _connection.StopAsync();
    public bool IsConnected() => !string.IsNullOrWhiteSpace(_connection.ConnectionId);

    private void ConfigureHandlers()
    {
        _connection.On<NotificationDto>("Notify", Notify);
    }

    public async Task Notify(NotificationDto dto)
    {
        _handler.Send(
            title: "Nova notificação",
            message: dto.Title);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _handler.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}