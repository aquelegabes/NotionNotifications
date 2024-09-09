using Hangfire;
using Hangfire.Storage.SQLite;
using NotionNotifications.Domain.Dtos;
using NotionNotifications.Domain.Interfaces;
using NotionNotifications.Integration;
using NotionNotifications.Server.Handlers;
using NotionNotifications.Server.Hubs;
using NotionNotifications.Server.Jobs;
using System.Text.Json;
using WebPush;

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
        app.UseHangfireDashboard("/hangfire");
        RecurringJob.AddOrUpdate<NotionIntegrationJobs>(
            recurringJobId: nameof(NotionIntegrationJobs.FetchAvailableNotificationsForCurrentDate),
            methodCall: (job) => job.FetchAvailableNotificationsForCurrentDate(),
            cronExpression: Cron.Daily());
    }

    public static void ConfigureServicesFromAssembly(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<NotionClient>();

        builder.Services.AddScoped<NotionIntegrationJobs>();

        builder.Services.AddScoped<IClientHandler, HubHelper>();

        builder.Services.AddSingleton<NotificationCollectionHandler>();

        builder.Services.AddSingleton<PwaSubscriptionsCollectionHandler>();
    }

    public static void MapHubs(
        this WebApplication app)
    {
        app.MapHub<NotionNotificationHub>("/notification");
    }

    public static void ConfigurePwaNotificationHandler(
        this WebApplication app)
    {
        app.MapGet("", () =>
        {
            return Results.Ok(new
            {
                alive = true
            });
        });

        app.MapPost("/pwa-test-notification", async (
            HttpContext context,
            PwaSubscriptionsCollectionHandler handler,
            NotificationDto model) =>
        {
            var vapidDetails = new VapidDetails(
                subject: Environment.GetEnvironmentVariable("VAPID_SUBJECT"),
                publicKey: Environment.GetEnvironmentVariable("VAPID_PUBLIC_KEY"),
                privateKey: Environment.GetEnvironmentVariable("VAPID_PRIVATE_KEY"));

            using var client = new WebPushClient();

            foreach (var subscription in handler.Subscriptions)
            {
                var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
                var payload = JsonSerializer.Serialize(new
                {
                    title = model.Title,
                    message = model.Message,
                    icon = model.Icon,
                });

                try
                {
                    await client.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                }
                catch (WebPushException ex)
                when (ex.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Gone)
                {
                    handler.Remove(subscription);
                }
            }

            return Results.Ok();
        });

        app.MapPut("/subscribe", (
            HttpContext context,
            PwaSubscriptionsCollectionHandler collectionHandler,
            WebPushNotificationSubscriptionDto subscription) =>
        {
            if (subscription is not null)
            {
                collectionHandler.Add(subscription);
                return Results.Accepted(value: subscription);
            }

            return Results.NotFound();
        });

        app.MapPut("/unsubscribe", (
            HttpContext context,
            PwaSubscriptionsCollectionHandler collectionHandler,
            WebPushNotificationSubscriptionDto subscription) =>
        {
            if (subscription is not null)
            {
                collectionHandler.RemoveIfExists(subscription);
                return Results.Ok();
            }

            return Results.NotFound();
        });
    }
}