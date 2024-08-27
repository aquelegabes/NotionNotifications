using NotionNotifications.External.Libnotify;

namespace NotionNotifications.WorkerService;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private const int DELAY_TIME_IN_SECONDS = 15;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var notification = new Notification(
                summary: "notificação de teste",
                body: "corpo da notificação",
                icon: Icons.Status.APPOINTMENT_SOON);

            var result = notification.Show();

            if (result.Shown)
            {
                _logger.LogInformation("Notification shown at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Notification summary: {summary}", notification.Summary);
                _logger.LogInformation("Notification body: {body}", notification.Body);
            }

            await Task.Delay(ToMilliseconds(DELAY_TIME_IN_SECONDS), stoppingToken);
        }
    }

    private static int ToMilliseconds(int seconds)
        => seconds * 1000;
}
