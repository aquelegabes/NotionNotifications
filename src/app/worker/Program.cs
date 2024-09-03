var builder = Host.CreateApplicationBuilder(args);

builder.Services.ConfigureServicesFromAssembly();
builder.Services.ConfigureBackgroundServicesFromAssembly();

var host = builder.Build();
host.Run();