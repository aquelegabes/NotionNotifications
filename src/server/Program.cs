using NotionNotifications.Integration;
using NotionNotifications.Server.Hubs;
using NotionNotifications.Server.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.ConfigureServices();

builder.ConfigureHangfire();

var app = builder.Build();

app.MapHub<NotionNotificationHub>("/notification");

// app.ConfigureRecurringJobs();

app.MapGet("/fetch", async(NotionIntegrationJobs job) => {
    await job.FetchAvailableNotificationsForCurrentDate();
});

app.Run();
