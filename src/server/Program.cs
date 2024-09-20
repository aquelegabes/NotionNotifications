const string POLICY_NAME = "NotionNotificationsAllowOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.ConfigureServicesFromAssembly();

builder.ConfigureHangfire();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy(POLICY_NAME, builder =>
    {
        builder.WithOrigins("https://localhost:7233", "http://localhost:5025")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors(POLICY_NAME);

app.MapHubs();

app.ConfigureRecurringJobs();

app.Run();
