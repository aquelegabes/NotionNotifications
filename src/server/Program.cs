using NotionNotifications.Server.Handlers;

const string POLICY_NAME = "NotionNotificationsAllowOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.ConfigureServicesFromAssembly();

builder.ConfigureHangfire();

var origins = builder.Configuration.GetSection("HostOrigins").Get<string[]>() ?? ["*"];
var additionalOrigins = builder.Configuration.GetValue<string>("ALLOWED_HOSTS")?.Split(';') ?? [];

builder.Services.AddCors(opts =>
{
    string[] allowed = [.. origins, .. additionalOrigins];
    opts.AddPolicy(POLICY_NAME, builder =>
    {
        builder.WithOrigins(allowed)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapGet("/", (HttpContext ctx, IConfiguration config) =>
{
    string[] allowedOrigins = [.. origins, .. additionalOrigins];
    return Results.Ok(new { allowedOrigins, alive = true });
});

app.MapGet("/subscriptions", (HttpContext ctx, PwaSubscriptionsHandler handler) => Results.Ok(handler.Find(_ => _.Auth is not null)));

app.UseCors(POLICY_NAME);

app.MapHubs();

app.ConfigureRecurringJobs();

app.Run();
