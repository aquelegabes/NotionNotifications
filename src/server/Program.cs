using System.Diagnostics;
using Hangfire;
using NotionNotifications.Data;
using NotionNotifications.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NotionNotificationsContext>();

builder.ConfigureHangfire();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/send-task", () => {

    BackgroundJob.Enqueue(() => Debug.WriteLine("[*] TEXTO ENVIADO PELO JOB ÀS: {0}", DateTimeOffset.Now));
    return "task sent";
});

app.Run();
