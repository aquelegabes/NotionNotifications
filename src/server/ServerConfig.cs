using Hangfire;
using Hangfire.Storage.SQLite;
using NotionNotifications.Integration;
using NotionNotifications.Server.Hubs;
using NotionNotifications.Server.Jobs;

public static class ServerConfig
{
    public static void ConfigureHangfire(
        this WebApplicationBuilder builder)
    {
        GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage();
    }

    public static void ConfigureRecurringJobs(
        this WebApplication app)
    {
        RecurringJob.AddOrUpdate<NotionIntegrationJobs>(
            recurringJobId: nameof(NotionIntegrationJobs.FetchAvailableNotificationsForCurrentDate),
            methodCall: (job) => job.FetchAvailableNotificationsForCurrentDate(),
            cronExpression: Cron.Daily());
    }

    public static void ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddSingleton<NotionClient>();

        builder.Services.AddSingleton<NotionIntegrationJobs>();
    }

    public static void MapHubs(
        this WebApplicationBuilder builder)
    {
        
    }
}