using NotionNotifications;
using NotionNotifications.Domain;
using NotionNotifications.WorkerService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<INotificationHandler, ConsoleNotificationHandler>();
builder.Services.AddSingleton<NotionNotificationsClientHub>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();