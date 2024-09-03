var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.ConfigureServicesFromAssembly();

builder.ConfigureHangfire();

var app = builder.Build();

app.MapHubs();

app.ConfigureRecurringJobs();

app.Run();
