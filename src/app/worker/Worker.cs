namespace NotionNotifications.WorkerService;

public class Worker(
    ILogger<Worker> logger,
    IHostApplicationLifetime hostApp,
    NotionNotificationsClientHub hub) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private const int DELAY_TIME_IN_SECONDS = 15;
    private const int MAX_CONNECTION_RETRIES = 3;

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);

        int tries = 1;

        while (!hub.IsConnected() && tries <= MAX_CONNECTION_RETRIES)
        {
            try
            {
                await hub.Connect();
                _logger.LogInformation("Connected to hub at: {time}", DateTimeOffset.Now);
            }
            catch (Exception)
            {
                _logger.LogError("Could not connect to hub at attempt: #{tries}", tries.ToString().PadLeft(2, '0'));
            }
            finally { tries++; }
        }

        if (hub.IsConnected())
        {
            await base.StartAsync(cancellationToken);
        }
        else
        {
            hostApp.StopApplication();
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(ToMilliseconds(DELAY_TIME_IN_SECONDS), stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);

        if (hub.IsConnected())
        {
            await hub.Disconnect();
            _logger.LogInformation("Disconnected from hub at: {time}", DateTimeOffset.Now);
        }

        hub.Dispose();
        _logger.LogInformation("Hub disposed at: {time}", DateTimeOffset.Now);

        await base.StopAsync(cancellationToken);
    }

    private static int ToMilliseconds(int seconds)
        => seconds * 1000;
}
