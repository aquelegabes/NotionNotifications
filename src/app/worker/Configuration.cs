using NotionNotifications;
using NotionNotifications.Domain;
using NotionNotifications.WorkerService;
using NotionNotifications.WorkerService.NotificationHandlers;

public static class Configuration
{
    public static void ConfigureServicesFromAssembly(
        this IServiceCollection services)
    {
        if (OperatingSystem.IsLinux())
        {
            services.AddSingleton<INotificationHandler, LibnotifyNotificationHandler>();
        }

        if (OperatingSystem.IsWindows())
        {
            services.AddSingleton<INotificationHandler, ConsoleNotificationHandler>();
        }

        services.AddSingleton<NotionNotificationsClientHub>();
    }

    public static void ConfigureBackgroundServicesFromAssembly(
        this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
    }
}