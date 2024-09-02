namespace NotionNotifications.WorkerService;

public class Worker(ILogger<Worker> logger, NotionNotificationsClientHub hub) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private const int DELAY_TIME_IN_SECONDS = 15;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!hub.IsConnected())
            {
                await hub.Connect();
            }

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(ToMilliseconds(DELAY_TIME_IN_SECONDS), stoppingToken);
        }
    }

    private static int ToMilliseconds(int seconds)
        => seconds * 1000;
}
