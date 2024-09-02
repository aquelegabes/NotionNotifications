using Hangfire;
using Hangfire.Storage.SQLite;
using NotionNotifications.Domain.Interfaces;
using NotionNotifications.Integration;
using NotionNotifications.Server.Hubs;
using NotionNotifications.Server.Jobs;

public static class ServerConfig
{
    public static void ConfigureHangfire(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(config =>
        {
            config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSQLiteStorage();
        });

        builder.Services.AddHangfireServer();
    }

    public static void ConfigureRecurringJobs(
        this WebApplication app)
    {
        app.UseHangfireDashboard("/jobs");
        RecurringJob.AddOrUpdate<NotionIntegrationJobs>(
            recurringJobId: nameof(NotionIntegrationJobs.FetchAvailableNotificationsForCurrentDate),
            methodCall: (job) => job.FetchAvailableNotificationsForCurrentDate(),
            cronExpression: Cron.Daily());
    }

    public static void ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<NotionClient>();

        builder.Services.AddScoped<NotionIntegrationJobs>();

        builder.Services.AddScoped<IClientHandler, HubHelper>();
    }

    public static void MapHubs(
        this WebApplication app)
    {
        app.MapHub<NotionNotificationHub>("/notification");
    }
}